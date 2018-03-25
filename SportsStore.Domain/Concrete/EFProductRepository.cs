using System.Linq;
using System.Security.Permissions;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }

            context.SaveChanges();
        }

        public Product DeleteProduct(int ProductID)
        {
            Product dbEntry = context.Products.Find(ProductID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            } 
            return dbEntry;
        }
    }
}