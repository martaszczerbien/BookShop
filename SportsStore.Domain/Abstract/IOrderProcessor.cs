using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Models.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}