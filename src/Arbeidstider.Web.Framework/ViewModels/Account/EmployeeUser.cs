using System;

namespace Arbeidstider.Web.Framework.ViewModels.Account
{
    public class EmployeeUser
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public int EmployeeID { get; set; }
        public Guid UserID { get; set; }
    }
}