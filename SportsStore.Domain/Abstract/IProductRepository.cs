using System.Linq;
using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Models.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int ProductID);
    }
}