using System.Data.Entity;
using Core.Domain.Models;
using Microsoft.Data.Entity.Metadata.Builders;

namespace Core.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            User.Configure(dbModelBuilder);
            RefreshToken.Configure(dbModelBuilder);
        }
    }
}