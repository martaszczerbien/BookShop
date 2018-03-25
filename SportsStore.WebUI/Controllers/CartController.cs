using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.WebUI.Models;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private IOrderProcessor _orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            _repository = repo;
            _orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {Cart = cart, ReturnUrl = returnUrl});
        }

        // GET: Cart
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string ReturnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            cart.AddItem(product, 1);

            return RedirectToAction("Index", new { ReturnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string ReturnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            cart.RemoveLine(product);

            return RedirectToAction("Index", new {ReturnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) 
        {
            if (cart.lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}