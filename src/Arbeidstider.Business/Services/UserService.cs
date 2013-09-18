using Arbeidstider.Business.Domain;

namespace Arbeidstider.Business.Services
{
    public class UserService
    {
        private static UserService _instance = null;

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserService();

                return _instance;
            }
        }

        private UserService()
        {
        }

        public bool VerifyUser(string user, string passwordHash)
        {
            if (user == null || passwordHash == null) return false;
            if (user == "test" && passwordHash == "test123") return true;
            return false;
        }
    }
}
