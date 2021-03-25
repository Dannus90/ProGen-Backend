using System;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            // Seeding depending on environment
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            if (environment == "Development")
            {
                Seed.SeedDataBase(dbModelBuilder);
            }
            
            Domain.Models.User.Configure(dbModelBuilder);
            Domain.Models.RefreshToken.Configure(dbModelBuilder);
        }
    }
}