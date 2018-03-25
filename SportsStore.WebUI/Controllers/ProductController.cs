using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.WebUI.Models;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsViewModel viewModel = new ProductsViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1)*PageSize) //from which element
                    .Take(PageSize),
                PaggingInfo = new PaggingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int productID)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}