using EfCore.OnModelCreating.WebApi.Abstractions;

namespace EfCore.OnModelCreating.WebApi.Models
{
    public sealed class Product:Entity
    {
       
        public string Name { get; set; } = string.Empty;
        public decimal price { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
       
    }
    public sealed class Category:Entity
    {
        public string Name { get; set; } = string.Empty;
    }
    public sealed class User:Entity
    {
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
    }
}
