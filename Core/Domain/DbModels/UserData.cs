using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("user_data")]
    public class UserData
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
        
        [Phone]
        [Column("phone_number", TypeName = "CHAR(64)")]
        public string? PhoneNumber { get; set; } = null;
        
        [EmailAddress]
        [Column("email_cv", TypeName = "CHAR(128)")]
        public string? EmailCv { get; set; } = null;
        
        [Column("city_sv", TypeName = "CHAR(128)")]
        public string? CitySv { get; set; } = null;
        
        [Column("city_en", TypeName = "CHAR(128)")]
        public string? CityEn { get; set; } = null;
        
        [Column("country_sv", TypeName = "CHAR(128)")]
        public string? CountrySv { get; set; } = null;
        
        [Column("country_en", TypeName = "CHAR(128)")]
        public string? CountryEn { get; set; } = null;
        
        [Column("profile_image", TypeName = "CHAR(256)")]
        public string? ProfileImage { get; set; } = null;
        
        [Column("profile_image_public_id", TypeName = "CHAR(36)")]
        public string? ProfileImagePublicId { get; set; } = null;
        
        [Column("created_at")] public DateTime CreatedAt { get; set; }

        [Column("updated_at")] public DateTime UpdatedAt { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .HasIndex(u => u.UserId)
                .IsUnique();
            
            modelBuilder.Entity<UserData>()
                .HasOne<User>();

            modelBuilder.Entity<UserData>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserData>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<UserData>()
                .Ignore(ud => ud.IdString);

            modelBuilder.Entity<UserData>()
                .Ignore(ud => ud.UserIdString);
        }
    }
}