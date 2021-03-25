using System.Data.Entity.Infrastructure;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class AppDbContext: DbContext
    {
        protected AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            Domain.Models.User.Configure(dbModelBuilder);
            Domain.Models.RefreshToken.Configure(dbModelBuilder);
        }
    }
}