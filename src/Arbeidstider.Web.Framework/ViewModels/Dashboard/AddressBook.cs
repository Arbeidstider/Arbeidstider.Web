using System.Collections.Generic;
using Arbeidstider.Business.Interfaces.Domain;

namespace Arbeidstider.Web.Framework.ViewModels.Dashboard
{
    public class AddressBook
    {
        public IEnumerable<IEmployeeUser> Colleagues { get; set; } 
        public readonly string[] Letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "X", "Y", "Z", "Å", "Ä", "Ö"};
    }
}
