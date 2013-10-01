using System.Collections.Generic;
using Arbeidstider.Web.Framework.ViewModels.Account;

namespace Arbeidstider.Web.Framework.ViewModels.Dashboard
{
    public class AddressBook
    {
        public IEnumerable<EmployeeUser> Colleagues { get; set; } 
        public readonly string[] Letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "X", "Y", "Z", "Å", "Ä", "Ö"};
    }
}
