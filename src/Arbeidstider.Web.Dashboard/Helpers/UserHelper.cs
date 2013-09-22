using System.Web;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Services.Models;
using Arbeidstider.Web.Services.Parameters;

namespace Arbeidstider.Web.Helpers
{
    internal static class UserHelper
    {
        public static bool IsLoggedIn(this HttpContext context)
        {
            var sessionParameters = new UserParameters(new User()
            {
                Username = context.GetSession(Constants.Session.USERNAME),
                Passwordhash = context.GetSession(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            var cookieParameters = new UserParameters(new User()
            {
                Username = context.GetCookie(Constants.Session.USERNAME), 
                Passwordhash = context.GetCookie(Constants.Session.PASSWORD_HASH)
            }).Parameters;
            bool loggedIn = UserService.Instance.VerifyUser(sessionParameters) || UserService.Instance.VerifyUser(cookieParameters);
            return loggedIn;
        }
    }
}