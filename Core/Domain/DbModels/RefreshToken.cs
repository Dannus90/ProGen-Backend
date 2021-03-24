
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Core.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        [Column("id", TypeName = "CHAR(36)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public static void Configure(DbModelBuilder builder)
        {
            
        }
    }
}