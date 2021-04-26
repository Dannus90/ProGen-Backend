using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("work_experience")]
    public class WorkExperience
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

        [Column("employment_rate", TypeName = "Char(54)")]
        public string EmployementRate { get; set; }
        
        [Column("company_name", TypeName = "Char(54)")]
        public string CompanyName { get; set; }
        
        [Column("role_sv", TypeName = "Char(108)")]
        public string RoleSv { get; set; }
        
        [Column("role_en", TypeName = "Char(108)")]
        public string RoleEn { get; set; }

        [Column("description_sv", TypeName = "TEXT")]
        public string DescriptionSv { get; set; }
        
        [Column("description_en", TypeName = "TEXT")]
        public string DescriptionEn { get; set; }
        
        [Column("city_sv", TypeName = "Char(72)")]
        public string CityCv { get; set; }
        
        [Column("city_en", TypeName = "Char(72)")]
        public string CityEn { get; set; }
        
        [Column("country_sv", TypeName = "Char(72)")]
        public string CountrySv { get; set; }
        
        [Column("country_en", TypeName = "Char(72)")]
        public string CountryEn { get; set; }
        
        [Column("date_started")] 
        public DateTime DateStarted { get; set; }

        [Column("date_ended")] 
        public DateTime DateEnded { get; set; }
        
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
            modelBuilder.Entity<WorkExperience>()
                .HasIndex(u => u.UserId)
                .IsUnique();
            
            modelBuilder.Entity<WorkExperience>()
                .HasOne<User>()
                .WithMany();
            
            modelBuilder.Entity<WorkExperience>().Property(u => u.DateStarted)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnUpdate();
            
            modelBuilder.Entity<WorkExperience>().Property(u => u.DateEnded)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnUpdate();

            modelBuilder.Entity<WorkExperience>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<WorkExperience>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<WorkExperience>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<WorkExperience>()
                .Ignore(u => u.UserIdString);
        }
    }
}