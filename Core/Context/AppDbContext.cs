using System;
using Core.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<UserPresentation> UserPresentation { get; set; }
        public DbSet<WorkExperience> WorkExperience { get; set; }
        public DbSet<Education> Education { get; set; }

        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            // Seeding depending on environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Development") Seed.SeedDataBase(dbModelBuilder);

            Domain.DbModels.User.Configure(dbModelBuilder);
            Domain.DbModels.RefreshToken.Configure(dbModelBuilder);
            Domain.DbModels.UserData.Configure(dbModelBuilder);
            Domain.DbModels.UserPresentation.Configure(dbModelBuilder);
            Domain.DbModels.WorkExperience.Configure(dbModelBuilder);
            Domain.DbModels.Education.Configure(dbModelBuilder);
        }
    }
}