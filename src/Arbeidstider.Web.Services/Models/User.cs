namespace Arbeidstider.Web.Services.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
    }

    public static class UserHelper
    {
        public static bool HasAccessToEmployer(this User user, int employerID)
        {
            return true;
        }
    }
}