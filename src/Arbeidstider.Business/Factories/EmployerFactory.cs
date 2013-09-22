using System.Data;
using Arbeidstider.Business.Domain;

namespace Arbeidstider.Business.Factories
{
    internal class EmployerFactory
    {
        internal static Employer Create(DataRow row)
        {
            var employer = new Employer();
            employer.Firstname = (string) row["Firstname"];
            employer.Lastname = (string) row["Lastname"];
            employer.Email = (string) row["Email"];
            employer.Mobile = (string) row["Email"];
            employer.Username = (string) row["Username"];

            return employer;
        }
    }
}
