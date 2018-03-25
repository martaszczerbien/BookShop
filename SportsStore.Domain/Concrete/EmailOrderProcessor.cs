using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using BookShop.WebUI.Models.Abstract;
using BookShop.WebUI.Models.Entities;

namespace BookShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "mailTestowy@gmail.com";
        public string MailFromAddress = "ksiegarnia@domena.pl";
        public bool UseSsl = true;
        public string Username = "UżytkownikSmtp";
        public string Password = "HasłoSmtp";
        public String ServerName = "smtp.przykład.pl";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\Users\Martka\Documents\maile_bookshop";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory; //dodanie pliku do zdefiniowanego katalogu
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Zamowienie")
                    .AppendLine("---")
                    .AppendLine("Zamowione ksiazki:");

                foreach (var line in cart.lines)
                {
                    var subtotal = line.Product.Price*line.Quantity;
                    body.AppendFormat("{0} x {1} (wartosc: {2:c})", line.Quantity, line.Product.Name, subtotal);
                }
                body.AppendFormat("Wartosc zamowienia: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Dane do wysylki:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Pakowanie prezentu: {0}", shippingDetails.GiftWrap ? "Tak" : "Nie");
                MailMessage mailMessage = new MailMessage(
                    _emailSettings.MailFromAddress,
                    _emailSettings.MailToAddress,
                    "Status zamowiena",
                    body.ToString());

                //if (_emailSettings.WriteAsFile)
                //{
                //    mailMessage.BodyEncoding = Encoding.ASCII;
                //}

                smtpClient.Send(mailMessage);
            }

        }
    }
}