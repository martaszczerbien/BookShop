using System.Web.Mvc;
using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            //controllerContext - dostep do wszystkich danych z klasy kontrolera, żądanie klienta
            //ModelBindingContext - dane na temat moodelu obiektów
        {
            Cart cart = (Cart) controllerContext.HttpContext.Session[sessionKey];

            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
    }