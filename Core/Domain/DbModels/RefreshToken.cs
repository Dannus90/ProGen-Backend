
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;

namespace Core.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Column("user_id", TypeName = "Char(36)")]
        public Guid UserId { get; set; }
        
        [Column("refresh_token", TypeName = "TEXT(500)")]
        [Required]
        public Guid Token { get; set; }
        
        [Column("token_set_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TokenSetAt { get; set; }
        
        /**
         * Model configurations. 
         */
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => x.Token);
        }
    }
}