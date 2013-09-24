using System.Collections.Generic;

namespace Arbeidstider.Common.Parameters
{
    public class UserParameters : IParameters
    {
        private readonly string _username;
        private readonly string _passwordhash;
        public UserParameters(string username, string passwordhash)
        {
            _username = username;
            _passwordhash = passwordhash;
            Create();
        }

        public void Create()
        {
            Parameters = new List<KeyValuePair<string, object>>()
            {
               new KeyValuePair<string, object>("@Username", _username),
               new KeyValuePair<string, object>("@Passwordhash", _passwordhash)
            };
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }
        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}