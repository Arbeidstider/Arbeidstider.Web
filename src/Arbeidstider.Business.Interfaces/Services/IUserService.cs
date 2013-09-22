using System.Collections.Generic;

namespace Arbeidstider.Business.Interfaces.Services
{
    public interface IUserService
    {
        bool VerifyUser(List<KeyValuePair<string, object>> userParameters);
    }
}
