using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)")]
        public Guid Id { get; set; }
        
        [Column("user_id", TypeName = "Char(36)")]
        public Guid UserId { get; set; }
        
        [Column("refresh_token", TypeName = "TEXT")]
        [Required]
        public Guid Token { get; set; }
        
        [Column("token_set_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TokenSetAt { get; set; }
        
        /**
         * Model configurations. 
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => x.Token);
        }
    }
}