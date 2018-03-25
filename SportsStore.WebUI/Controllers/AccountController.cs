using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.WebUI.Infrastructure.Abstract;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.Username, model.Password))
                {
                    return RedirectToAction("Index", "Admin");
                    //return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub niepoprawne hasło.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Account

    }
}