namespace Arbeidstider.Web.Services.Models
{
    public class User
    {
    }

    public static class UserHelper
    {
        public static bool HasAccessToEmployer(this User user, int employerID)
        {
            return true;
        }
    }
}