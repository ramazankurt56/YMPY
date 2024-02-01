using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.Relational.WebApi.Models
{
    //[Index("Name",IsUnique =true)]
    public sealed class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public AdditionalProduct? AdditionalProduct { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Category? Category { get; set; }
    }
    public sealed class AdditionalProduct
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "Money")]
        public decimal? Price {  get; set; }
    }
}
