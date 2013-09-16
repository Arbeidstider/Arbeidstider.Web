using System;
using System.Web;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.Models;

namespace Arbeidstider.Web.Helpers
{
    public class UserHelper
    {
        public static void LoginEmployer(LoginModel user)
        {
            string username = user.UserName;
            string passwordhash = PasswordHelper.Hashpassword(user.Password);

            HttpContext.Current.Session["Username"] = username;
            HttpContext.Current.Session["Passwordhash"] = passwordhash;
            if (!user.RememberMe) return;

            SetUsername(user.UserName);
            SetPasswordHash(passwordhash);

        }

        private static void SetPasswordHash(string passwordhash)
        {
            if (HttpContext.Current.Response.Cookies["Passwordhash"] != null)
            {
                HttpContext.Current.Response.Cookies["Passwordhash"].Value = passwordhash;
                HttpContext.Current.Response.Cookies["Passwordhash"].Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                HttpContext.Current.Response.AppendCookie(new HttpCookie("Passwordhash", passwordhash));
                HttpContext.Current.Response.Cookies["Passwordhash"].Expires = DateTime.Now.AddDays(7);
            }
        }

        private static void SetUsername(string username)
        {
            if (HttpContext.Current.Response.Cookies["Username"] != null)
            {
                HttpContext.Current.Response.Cookies["Username"].Value = username;
                HttpContext.Current.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                HttpContext.Current.Response.AppendCookie(new HttpCookie("Username", username));
                HttpContext.Current.Response.Cookies["username"].Expires = DateTime.Now.AddDays(7);
            }
        }

        public static void LogOff()
        {
            HttpContext.Current.Session[Constants.Session.USERNAME] = null;
            HttpContext.Current.Session[Constants.Session.PASSWORD_HASH] = null;

            HttpContext.Current.Response.Cookies.Remove(Constants.Session.USERNAME);
            HttpContext.Current.Response.Cookies.Remove(Constants.Session.PASSWORD_HASH);
        }
    }
}