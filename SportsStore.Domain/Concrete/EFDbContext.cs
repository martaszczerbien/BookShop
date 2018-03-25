using System.Data.Entity;
using BookShop.WebUI.Models.Entities;

namespace BookShop.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
         public DbSet<Product> Products { get; set; } 
    }
}