using System;
using System.Web;
using System.Web.SessionState;
using Arbeidstider.Web.Constants;

namespace Arbeidstider.Web.Helpers
{
    public static class WebHelper
    {
        public static void SetCookie(this HttpContextBase context, string key, object value, DateTime? expires = null)
        {
            SetCookie(context.Response, key, value, expires);
        }

        public static void SetCookie(this HttpContext context, string key, object value, DateTime? expires = null)
        {
            SetCookie(context.Response, key, value, expires);
        }

        public static void SetCookie(HttpResponseBase response, string key, object value, DateTime? expires = null)
        {
            if (expires != null)
            {
                if (response.Cookies[Cookie.Key] == null) response.Cookies.Add(new HttpCookie(Cookie.Key));
                response.Cookies[Cookie.Key][key] = (string)value;
                response.Cookies[Cookie.Key].Expires = expires.Value;
                return;
            }
            if (response.Cookies[Cookie.Key] != null) response.Cookies.Remove(Cookie.Key);
        }

        public static void SetCookie(HttpResponse response, string key, object value, DateTime? expires = null)
        {
            if (expires != null)
            {
                if (response.Cookies[Cookie.Key] == null) response.Cookies.Add(new HttpCookie(Cookie.Key));
                response.Cookies[Cookie.Key][key] = (string)value;
                response.Cookies[Cookie.Key].Expires = expires.Value;
                return;
            }
            if (response.Cookies[Cookie.Key] != null) response.Cookies.Remove(Cookie.Key);
        }

        public static void SetSession(this HttpContextBase context, string key, object value)
        {
            SetSession(context.Session, key, value);
        }

        public static void SetSession(this HttpContext context, string key, object value)
        {
            SetSession(context.Session, key, value);
        }

        public static void SetSession(HttpSessionStateBase session, string key, object value)
        {
            session[key] = null;
            session[key] = (string)value;
        }

        public static void SetSession(HttpSessionState session, string key, object value)
        {
            session[key] = null;
            session[key] = (string)value;
        }

        public static string GetSession(this HttpContext context, string key)
        {
            if (context.Session == null || context.Session[key] == null) return null;
            return (string) context.Session[key];
        }

        public static string GetCookie(this HttpContext context, string key)
        {
            string cookie = null;
            if (context.Request.Cookies[Cookie.Key] != null) 
                cookie = (string) context.Request.Cookies[Cookie.Key][key];

            return cookie;
        }
    }
}