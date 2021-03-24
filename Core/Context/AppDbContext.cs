using System.Data.Entity;
using Core.Domain.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using DbContext = System.Data.Entity.DbContext;

namespace Core.Context
{
    public class AppDbContext: DbContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            User.Configure(dbModelBuilder);
            RefreshToken.Configure(dbModelBuilder);
        }
    }
}