using System.Data.Entity;
using Core.Domain.Models;

namespace Core.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            User.Configure(modelBuilder);
            RefreshToken.Configure(modelBuilder);
        }
    }
}