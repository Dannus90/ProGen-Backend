using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostContext, builder) =>
                    {
                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            builder.AddUserSecrets<Program>();
                        }
                    });
                    
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}