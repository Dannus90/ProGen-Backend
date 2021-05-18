using System;
using System.Text;
using API.Helpers.DbHelpers;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Configurations;
using Core.Context;
using Core.Mapping;
using Dapper;
using Infrastructure.configurations;
using Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private const string _allowedSpecificOrigins = "_allowedSpecificOrigins";
        private readonly string _connectionString;
        private readonly DependencyInjection _dependencyInjection;
        private readonly IConfigurationSection _proGenConfig;
        private readonly IConfigurationSection _tokenConfig;
        private readonly IConfiguration _cloudinaryConfig;
        private readonly IConfiguration _sendGridConfig;
        private readonly IConfiguration _progenUrlConfig;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Properties/appsettings.json", false, true)
                .AddJsonFile($"Properties/appsettings.{env.EnvironmentName}.json", false,
                    true).AddEnvironmentVariables();

            _configuration = configurationBuilder.Build();
            _proGenConfig = _configuration.GetSection("ProGenConfig");
            _cloudinaryConfig = _configuration.GetSection("CloudinaryConfig");
            _sendGridConfig = _configuration.GetSection("SendGrid");
            _progenUrlConfig = _configuration.GetSection("ProGenUrlConfig");

            // Access from env variables. 
            // Read more: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=linux&fbclid=IwAR1_Ih_VPv4SPDaKZkKwIU0-nrixJU8vfiLvYvbPrmXovt39jwvvpsCjfXg#register-the-user-secrets-configuration-source
            // Read more: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=linux&fbclid=IwAR1_Ih_VPv4SPDaKZkKwIU0-nrixJU8vfiLvYvbPrmXovt39jwvvpsCjfXg#secret-manager
            _cloudinaryConfig.GetSection("ApiKey").Value = configuration["CloudinaryConfig:ApiKey"];
            _cloudinaryConfig.GetSection("ApiSecret").Value = configuration["CloudinaryConfig:ApiSecret"];
            _cloudinaryConfig.GetSection("CloudName").Value = configuration["CloudinaryConfig:CloudName"];
            _sendGridConfig.GetSection("ApiKey").Value = configuration["SendGrid:ApiKey"];

            // Some secrets come from environment and are set on the hosting platform (or locally) for security reasons.
            if (env.IsDevelopment() | env.IsEnvironment("TestCi"))
            {
                _connectionString = _proGenConfig.Get<ProGenConfig>().DbConnectionString;
                _tokenConfig = _configuration.GetSection("TokenConfig");
            }
            else
            {
                _connectionString = _configuration["DB_CONNECTION_STRING"];

                if (_connectionString == null)
                {
                    _connectionString = _connectionString = _proGenConfig.Get<ProGenConfig>().DbConnectionString;
                }
                
                _tokenConfig = _configuration.GetSection("TokenConfig");
                _tokenConfig.GetSection("SecretKey").Value = configuration["SecretKey"];
            }
            
            _dependencyInjection = new DependencyInjection();
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register database.
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(_connectionString, b =>
                    b.MigrationsAssembly("API")));

            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    _allowedSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                                "https://localhost:3000",
                                "http://localhost:4000",
                                "https://localhost:4000",
                                "https://progen-frontend.herokuapp.com",
                                "http://progen-frontend.herokuapp.com",
                                "https://progen-gql-apollo-server.herokuapp.com",
                                "http://progen-gql-apollo-server.herokuapp.com")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            });

            SqlMapper.AddTypeHandler(new TrimmedStringHandler());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _tokenConfig.Get<TokenConfig>().Issuer,
                        ValidAudience = _tokenConfig.Get<TokenConfig>().Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig
                                .Get<TokenConfig>()
                                .SecretKey))
                    };
                });

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new SimpleMappers()); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(GetType().Assembly);
            services.AddControllers(opt => opt.Filters.Add(new ExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "API", Version = "v1"});
            });
            
            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            // Dependency injection.
            services.Configure<ProGenConfig>(_proGenConfig);
            services.Configure<TokenConfig>(_tokenConfig);
            services.Configure<CloudinaryConfig>(_cloudinaryConfig);
            services.Configure<SendGridConfig>(_sendGridConfig);
            services.Configure<ProGenUrlConfig>(_progenUrlConfig);
            _dependencyInjection.AddDependencyInjectionHandlers(services);
            DependencyInjection.AddDependencyInjectionServices(services);
            DependencyInjection.AddDependencyInjectionRepositories(services, _connectionString, mapper);
        }

        // This method gets called by the runtime. We use this method to configure the pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(_allowedSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}