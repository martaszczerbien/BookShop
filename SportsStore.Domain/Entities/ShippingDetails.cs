using System.ComponentModel.DataAnnotations;

namespace BookShop.WebUI.Models.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Podaj nazwisko.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Podaj adres.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Podaj nazwe miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Podaj nazwe wojewodztwa")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Podaj nazwe kraju.")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}