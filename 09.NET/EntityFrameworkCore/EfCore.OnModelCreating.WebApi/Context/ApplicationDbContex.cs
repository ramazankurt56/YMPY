using EfCore.OnModelCreating.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EfCore.OnModelCreating.WebApi.Context
{
    public class ApplicationDbContex:DbContext
    {
        public ApplicationDbContex(DbContextOptions options):base(options) { }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<Product>().Property(p => p.price).IsRequired().HasColumnType("money");
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).IsRequired();
            modelBuilder.Entity<Product>().HasOne(p=>p.Category).WithMany().HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Category>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().Property(p=>p.Name).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Entity<Category>().HasIndex(p=>p.Name).IsUnique(true);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasIndex(p=>p.Email).IsUnique(true);
            modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(p => p.LastName).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).HasColumnType("varchar(200)").IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnType("varchar(10)");

        }
    }
}
