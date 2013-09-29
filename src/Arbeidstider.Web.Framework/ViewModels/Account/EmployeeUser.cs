using System;
using Arbeidstider.Business.Logic.Enums;

namespace Arbeidstider.Web.Framework.ViewModels.Account
{
    public class EmployeeUser
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public int EmployeeID { get; set; }
        public Guid UserID { get; set; }
        public EmployeeGroup EmployeeGroup { get; set; }

        public bool IsAdmin()
        {
            return EmployeeGroup == EmployeeGroup.Administrator;
        }
    }
}