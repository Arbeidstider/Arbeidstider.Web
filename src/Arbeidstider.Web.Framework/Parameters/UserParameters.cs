using System.Collections.Generic;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class UserParameters
    {
        private readonly string _username;
        public UserParameters(string username)
        {
            _username = username;
            Create();
        }

        public void Create()
        {
            Parameters = new List<KeyValuePair<string, object>>()
            {
               new KeyValuePair<string, object>("@Username", _username)
            };
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }
        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}