using System.Collections;
using System.Collections.Generic;
using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Models
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaggingInfo PaggingInfo { get; set; } 
        public string CurrentCategory { get; set; }
    }
}