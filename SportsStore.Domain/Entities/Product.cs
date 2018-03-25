using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookShop.WebUI.Models.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Podaj tytul ksiazki.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Podaj opis ksiazki.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Podaj dodatnia cene")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Podaj kategorie.")]
        public string Category { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}