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
        [Column("id", TypeName = "CHAR(36)", Order = 1)]
        public Guid Id { get; set; }

        public string IdString
        {
            get => Id.ToString("N");
            set => Id = new Guid(value);
        }

        [Column("user_id", TypeName = "Char(36)")]
        [Required]
        public Guid UserId { get; set; }

        public string UserIdString
        {
            get => UserId.ToString("N");
            set => UserId = new Guid(value);
        }

        [Column("refresh_token", TypeName = "TEXT")]
        [Required]
        public string Token { get; set; }

        [Column("token_set_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TokenSetAt { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rf => rf.UserId)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(x => x.Token);

            modelBuilder.Entity<RefreshToken>()
                .Property(u => u.TokenSetAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<RefreshToken>()
                .HasOne<User>();

            modelBuilder.Entity<RefreshToken>()
                .Ignore(rf => rf.IdString);

            modelBuilder.Entity<RefreshToken>()
                .Ignore(rf => rf.UserIdString);
        }
    }
}