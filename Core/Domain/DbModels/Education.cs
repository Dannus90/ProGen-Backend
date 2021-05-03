using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("education")]
    public class Education
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

        [Column("education_name_sv", TypeName = "Char(108)")]
        public string EducationNameSv { get; set; }
        
        [Column("education_name_en", TypeName = "Char(108)")]
        public string EducationNameEn { get; set; }
        
        [Column("exam_name_sv", TypeName = "Char(108)")]
        public string ExamNameSv { get; set; }
        
        [Column("exam_name_en", TypeName = "Char(108)")]
        public string ExamNameEn { get; set; }
        
        [Column("subject_area_sv", TypeName = "Char(108)")]
        public string SubjectAreaSv { get; set; }
        
        [Column("subject_area_en", TypeName = "Char(108)")]
        public string SubjectAreaEn { get; set; }

        [Column("description_sv", TypeName = "TEXT")]
        public string DescriptionSv { get; set; }
        
        [Column("description_en", TypeName = "TEXT")]
        public string DescriptionEn { get; set; }
        
        [Column("grade", TypeName = "Char(72)")]
        public string Grade { get; set; }
        
        [Column("city_sv", TypeName = "Char(72)")]
        public string CitySv { get; set; }
        
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
            modelBuilder.Entity<Education>()
                .HasIndex(u => u.UserId);
            
            modelBuilder.Entity<Education>()
                .HasOne<User>()
                .WithMany();
            
            modelBuilder.Entity<Education>().Property(u => u.DateStarted)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnUpdate();
            
            modelBuilder.Entity<Education>().Property(u => u.DateEnded)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnUpdate();

            modelBuilder.Entity<Education>().Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Education>().Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Education>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<Education>()
                .Ignore(u => u.UserIdString);
        }
    }
}