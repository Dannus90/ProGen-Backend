using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("skill")]
    public class Skill
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)", Order = 1)]
        public Guid Id { get; set; }

        public string IdString
        {
            get => Id.ToString("N");
            set => Id = new Guid(value);
        }

        [Column("skill_name", TypeName = "CHAR(128)")]
        [Required]
        public string SkillName { get; set; } = null!;

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasIndex(x => x.SkillName);
            
                        
            modelBuilder.Entity<Skill>()
                .Ignore(u => u.IdString);
        }
    }
}