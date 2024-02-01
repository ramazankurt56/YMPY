using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.Relational.WebApi.Models
{
    [Index("Name",IsUnique =true)]
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }=string.Empty;
    }
}
