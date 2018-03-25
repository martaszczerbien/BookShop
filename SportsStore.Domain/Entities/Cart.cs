using System.Collections.Generic;
using System.Linq;

namespace BookShop.WebUI.Models.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product prod, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == prod.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine {Product = prod, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product prod)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == prod.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> lines 
        {
            get { return lineCollection; }
        } 
    }
}