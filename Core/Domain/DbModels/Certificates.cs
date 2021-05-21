using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("certificates")]
    public class Certificate
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

        [Column("certificate_name_sv", TypeName = "Char(108)")]
        public string CertificateNameSv { get; set; } = null!;
        
        [Column("certificate_name_en", TypeName = "Char(108)")]
        public string CertificateNameEn { get; set; } = null!;
        
        [Column("organisation", TypeName = "Char(108)")]
        public string Organisation { get; set; } = null!;
        
        [Column("identification_id", TypeName = "Char(108)")]
        public string IdentificationId { get; set; } = null!;
        
        [Column("reference_address", TypeName = "Char(108)")]
        public string ReferenceAddress { get; set; } = null!;
        
        [Column("date_issued")] 
        public DateTime DateIssued { get; set; }

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
            modelBuilder.Entity<Certificate>()
                .HasIndex(u => u.UserId);
            
            modelBuilder.Entity<Certificate>()
                .HasOne<User>()
                .WithMany();
            
            modelBuilder.Entity<Certificate>().Property(u => u.DateIssued)
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