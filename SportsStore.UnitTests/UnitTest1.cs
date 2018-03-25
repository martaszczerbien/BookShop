using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookShop.WebUI.Controllers;
using BookShop.WebUI.HtmlHelpers;
using BookShop.WebUI.Models;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //działanie
            ProductsViewModel result = (ProductsViewModel)controller.List(null,2).Model;

            //assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Generation_Page_Links()
        {
            PaggingInfo paginginfo = new PaggingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            HtmlHelper myHelper = null;

            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            MvcHtmlString result = myHelper.PageLinks(paginginfo, pageUrlDelegate);

            Assert.AreEqual(result.ToString(), @"<a href=""Strona1"">1</a><a class=""selected"" href=""Strona2"">2</a><a href=""Strona3"">3</a>");
        }

        //[TestMethod]
        //public void Can_Send_Pagination_View_Model()
        //{
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1"},
        //        new Product {ProductID = 2, Name = "P2"},
        //        new Product {ProductID = 3, Name = "P3"},
        //        new Product {ProductID = 4, Name = "P4"},
        //        new Product {ProductID = 5, Name = "P5"}
        //    }.AsQueryable());

        //    ProductController controller = new ProductController(mock.Object);
        //    controller.PageSize = 3;

        //    var result = (ProductsViewModel)controller.List(2).Model;

        //    Product[] prodArray = result.Products.ToArray();
        //    Assert.IsTrue(prodArray.Length ==2);
        //    Assert.AreEqual("P4", prodArray[0].Name);
        //    Assert.AreEqual("P5", prodArray[1].Name);
        //}

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "cat5"}
            }.AsQueryable());

            ProductController controler = new ProductController(mock.Object);
            controler.PageSize = 3;

            Product[] result = ((ProductsViewModel)controler.List("cat2", 1).Model).Products.ToArray();

            Assert.IsTrue(result.Length == 2);
            Assert.IsTrue(result[0].Name =="P2" && result[0].Category == "cat2");
            Assert.IsTrue(result[1].Name=="P4" && result[1].Category=="cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                new Product {ProductID = 2, Name = "P2", Category = "Jabłka"},
                new Product {ProductID = 3, Name = "P3", Category = "Śliwki"},
                new Product {ProductID = 4, Name = "P4", Category = "Pomarańcze"}
            }.AsQueryable());

            NavController controller = new NavController(mock.Object);

            string[] result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.IsTrue(result[0]=="Jabłka");
            Assert.IsTrue(result[1]=="Pomarańcze");
            Assert.IsTrue(result[2] == "Śliwki");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                new Product {ProductID = 2, Name = "P2", Category = "Pomarańcze"}
            }.AsQueryable());

            NavController controller = new NavController(mock.Object);

            string categoryToSelect = "Jabłka";
            var result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock <IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            int res1 = ((ProductsViewModel) controller.List("Cat1").Model).PaggingInfo.TotalItems;
            int res2 = ((ProductsViewModel) controller.List("Cat2").Model).PaggingInfo.TotalItems;
            int res3 = ((ProductsViewModel) controller.List("Cat3").Model).PaggingInfo.TotalItems;
            int res4 = ((ProductsViewModel) controller.List(null).Model).PaggingInfo.TotalItems;

            Assert.AreEqual(2, res1);
            Assert.AreEqual(2, res2);
            Assert.AreEqual(1, res3);
            Assert.AreEqual(5, res4);
        }
    }
}
