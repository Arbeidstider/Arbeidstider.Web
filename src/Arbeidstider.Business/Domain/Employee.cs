using System;

namespace Arbeidstider.Business.Domain
{
    public class Employer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Enum EmployeeGroup { get; set; }
        public string Fullname { get { return Firstname + " " + Lastname; }}
        public Workplace Workplace { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public bool RememberMe { get; set; }
    }

    public static class EmployerHelper
    {
        public static bool HasAccessToWorkplace(this Employer employer, int workplaceID)
        {
            return true;
        }
    }
}