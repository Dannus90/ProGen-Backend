using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("last_login")] public DateTime? LastLogin { get; set; } = null;

        public static void Configure(DbModelBuilder builder)
        {
            
        }
    }
}