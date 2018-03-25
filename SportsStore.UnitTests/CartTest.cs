using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookShop.WebUI.Controllers;
using BookShop.WebUI.Models;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Adding_New_Lines()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] result = target.lines.ToArray();

            Assert.AreEqual("P1", result[0].Product.Name);
            Assert.AreEqual("P2", result[1].Product.Name);
        }

        [TestMethod]
        public void Increasing_Quantity_For_Existing_Lines()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] result = target.lines.ToArray();

            Assert.IsTrue(result.Length == 2);
            Assert.IsTrue(result[0].Quantity == 11);
            Assert.IsTrue(result[1].Quantity == 1);
        }

        [TestMethod]
        public void Removing_Line()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            target.RemoveLine(p1);

            CartLine[] result = target.lines.ToArray();

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(result[0].Quantity == 1);
        }

        [TestMethod]
        public void Summ_Of_Prices()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10};
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 1};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 9);
            var total = target.ComputeTotalValue();

            Assert.AreEqual(total, 101M);
        }

        [TestMethod]
        public void Cleaning_Up_cart()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 1 };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 9);
            target.Clear();

            Assert.AreEqual(target.lines.Count(), 0);
        }

        //[TestMethod]
        //public void Add_To_Cart()
        //{
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"}
        //    }.AsQueryable());

        //    Cart cart = new Cart();

        //    CartController controller = new CartController(mock.Object);

        //    controller.AddToCart(cart, 1, null);

        //    Assert.AreEqual(cart.lines.Count(), 1);
        //    Assert.AreEqual(cart.lines.ToArray()[0].Product.Name, "P1");
        //}

        //[TestMethod]
        //public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        //{
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"}
        //    }.AsQueryable());
        //    Cart cart = new Cart();

        //    CartController controler = new CartController(mock.Object);

        //    RedirectToRouteResult result = controler.AddToCart(cart, 2, "myUrl");

        //    Assert.AreEqual(result.RouteValues["action"], "Index");
        //    Assert.AreEqual(result.RouteValues["ReturnUrl"], "myUrl");
        //}

        //[TestMethod]
        //public void Can_View_Cart_Content()
        //{
        //    CartController controller = new CartController(null);

        //    Cart cart = new Cart();

        //    CartIndexViewModel result = (CartIndexViewModel) controller.Index(cart, "myUrl").ViewData.Model;

        //    Assert.AreSame(cart, result.Cart);
        //    Assert.AreEqual("myUrl", result.ReturnUrl);
        //}

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart(); 

            ShippingDetails shippingDetails = new ShippingDetails();

            CartController target = new CartController(null, mock.Object);

            ViewResult result = target.Checkout(cart, shippingDetails);

            //sprawdzenie czy zamowienie zostalo przekazane do procesora
            //czy metoda nie jest nigdy wywołana
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            //ponowne wyświetlenie widoku
            Assert.AreEqual("", result.ViewName);

            //stan modelu jest nieprawidłowy
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            CartController target = new CartController(null, mock.Object);

            target.ModelState.AddModelError("error", "error");

            ViewResult result = target.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            Assert.AreEqual("", result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 2);

            CartController target = new CartController(null, mock.Object);

            ViewResult result = target.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        
    }

    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product[] result = ((IEnumerable<Product>) target.Index().ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }
    }

}

   

