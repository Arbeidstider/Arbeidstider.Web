using System.Collections.Generic;

namespace Arbeidstider.Web.Services.Parameters
{
    public interface IParameters
    {
        void Create();
        List<KeyValuePair<string, object>> Parameters { get; set; }
        void Validate();
    }
}