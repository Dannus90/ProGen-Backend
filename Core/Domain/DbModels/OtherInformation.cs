using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("other_information")]
    public class OtherInformation
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

        [Column("driving_license_sv", TypeName = "TEXT")]
        public string DrivingLiscenseSv { get; set; }
        
        [Column("driving_license_en", TypeName = "TEXT")]
        public string DrivingLiscenseEn { get; set; }
        
        [Column("languages_sv", TypeName = "TEXT")]
        public string LanguagesSv { get; set; }
        
        [Column("languages_en", TypeName = "TEXT")]
        public string LanguagesEn { get; set; }

        [Column("created_at")] 
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")] 
        public DateTime UpdatedAt { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherInformation>()
                .HasIndex(u => u.UserId);
            
            modelBuilder.Entity<OtherInformation>()
                .HasOne<User>()
                .WithOne();

            modelBuilder.Entity<Education>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Education>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<OtherInformation>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<OtherInformation>()
                .Ignore(u => u.UserIdString);
        }
    }
}