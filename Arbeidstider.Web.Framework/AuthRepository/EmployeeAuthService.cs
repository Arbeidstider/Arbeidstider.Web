using Arbeidstider.DataAccess.Domain;
using Arbeidstider.Interfaces;
using ServiceStack.Auth;
using ServiceStack.Data;

namespace Arbeidstider.Web.Framework.AuthRepository
{
public class EmployeeAuthRepository : OrmLiteAuthRepository, IUserAuthRepository
{
    public EmployeeAuthRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
    {
    }

    public EmployeeAuthRepository(IDbConnectionFactory dbFactory, IHashProvider passwordHasher) : base(dbFactory, passwordHasher)
    {
    }

    public IUserAuth CreateUserAuth(UserAuth newUser, string password, int workplaceId)
    {
        newUser.Set((IEmployee)new Employee() { WorkplaceId = workplaceId});
        return base.CreateUserAuth(newUser, password);
    }
}
}
