using System.Collections.Generic;
using Arbeidstider.Web.Services.Models;

namespace Arbeidstider.Web.Services.Parameters
{
    public class UserParameters : IParameters
    {
        private readonly User _user;
        public UserParameters(User user)
        {
            _user = user;
            Create();
        }

        public void Create()
        {
            Parameters = new List<KeyValuePair<string, object>>()
            {
               new KeyValuePair<string, object>("@Username", _user.Username),
               new KeyValuePair<string, object>("@Passwordhash", _user.Passwordhash)
            };
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }
        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}