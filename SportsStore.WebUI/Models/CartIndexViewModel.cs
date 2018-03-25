using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}