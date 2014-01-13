using ServiceStack;

namespace Arbeidstider.Web.Services.Attributes
{
    public class RequiredEmployeeRole : RequiredRoleAttribute
    {
        // If role is manager, check against workplaceID
        public override void Execute(ServiceStack.Web.IRequest req, ServiceStack.Web.IResponse res, object requestDto)
        {
            base.Execute(req, res, requestDto);
        }
    }
}