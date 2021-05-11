using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("user_presentation")]
    public class UserPresentation
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
        
        [Column("presentation_sv", TypeName = "TEXT")]
        [Required]
        public string PresentationSv { get; init; } = null!;
        
        [Column("presentation_en", TypeName = "TEXT")]
        [Required]
        public string PresentationEn { get; init; } = null!;
        
        [Column("created_at")] public DateTime CreatedAt { get; set; }

        [Column("updated_at")] public DateTime UpdatedAt { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPresentation>()
                .HasIndex(u => u.UserId)
                .IsUnique();
            
            modelBuilder.Entity<UserPresentation>()
                .HasOne<User>();

            modelBuilder.Entity<UserPresentation>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserPresentation>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<UserPresentation>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<UserPresentation>()
                .Ignore(u => u.UserIdString);
        }
    }
}