using System.Collections.Generic;
using Arbeidstider.Web.Framework.Parameters;

namespace Arbeidstider.Common.Parameters
{
    public class UserParameters : IParameters
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