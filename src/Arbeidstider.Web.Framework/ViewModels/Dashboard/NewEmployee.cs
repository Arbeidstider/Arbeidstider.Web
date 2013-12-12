
namespace Arbeidstider.Web.Framework.ViewModels.Dashboard
{
    public class NewEmployee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string BirthDate { get; set; }
        public int EmployeeGroup { get; set; }
        public int WorkplaceID { get; set; }

        public string GenerateUsername()
        {
            string day = BirthDate.Substring(0, 2);
            string month = BirthDate.Substring(3, 2);
            string firstname = Firstname.Substring(0, 2);
            string lastname = Lastname.Substring(0, 2);
            return firstname.ToLower() + lastname.ToLower() + day + month;
        }

        public bool? Success { get; set; }
        public string GeneratePassword()
        {
            return "Test123!";
        }
    }
}
