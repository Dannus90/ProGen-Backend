using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace Core.Domain.Models
{
    public class User
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        [Column("email", TypeName = "CHAR(128)")]
        public string Email { get; set; }
        
        [Required]
        [Column("password", TypeName = "CHAR(500)")]
        public string Password { get; set; }
        
        [Column("last_login")] public DateTime? LastLogin { get; set; } = null;
        
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
        
        /**
         * Model configurations. 
         */
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}