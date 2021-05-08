using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("language")]
    public class Language
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

        [Column("language_sv", TypeName = "Char(128)")]
        public string LanguageSv { get; set; }
        
        [Column("language_en", TypeName = "Char(128)")]
        public string LanguageEn { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>()
                .HasIndex(u => u.UserId);

            modelBuilder.Entity<Language>()
                .HasOne<User>()
                .WithMany();

            modelBuilder.Entity<Language>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<Language>()
                .Ignore(u => u.UserIdString);
        }
    }
}