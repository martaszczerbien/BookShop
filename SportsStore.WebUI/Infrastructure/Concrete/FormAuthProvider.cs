using System.Web.Security;
using BookShop.WebUI.Infrastructure.Abstract;

namespace BookShop.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string userName, string password)
        {
            bool result = FormsAuthentication.Authenticate(userName, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            return result;
        }
    }
}