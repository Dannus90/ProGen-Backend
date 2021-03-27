using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("user_base")]
    public class User
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)")]
        public Guid Id { get ; set; }

        public string IdString => Id.ToString("N");

        [Required]
        [Column("email", TypeName = "CHAR(128)")]
        public string Email { get; set; }
        
        [Required]
        [Column("password", TypeName = "CHAR(500)")]
        public string Password { get; set; }
        
        [Column("last_login")] public DateTime? LastLogin { get; set; } = null;
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        /**
         * Model configurations. 
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<User>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}