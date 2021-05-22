using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.DbModels
{
    [Table("user_skill")]
    public class UserSkill
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
        
        [Column("skill_id", TypeName = "Char(36)")]
        [Required]
        public Guid SkillId { get; set; }

        public string SkillIdString
        {
            get => SkillId.ToString("N");
            set => SkillId = new Guid(value);
        }
        
        [Column("level", TypeName = "SMALLINT")]
        [Required]
        [Range(1, 5)]
        public int Level { get; set; }

        /**
         * Model configurations.
         *
         * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
         */
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>()
                .HasIndex(x => x.SkillId);
            
            modelBuilder.Entity<UserSkill>()
                .HasIndex(x => x.UserId);

            modelBuilder.Entity<UserSkill>().HasOne<Skill>()
                .WithMany();
            
            modelBuilder.Entity<UserSkill>().HasOne<User>()
                .WithMany();
            
            modelBuilder.Entity<UserSkill>()
                .Ignore(u => u.IdString);
            
            modelBuilder.Entity<UserSkill>()
                .Ignore(u => u.UserIdString);
            
            modelBuilder.Entity<UserSkill>()
                .Ignore(u => u.SkillIdString);
        }
    }
}