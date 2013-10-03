using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Business.Logic.IoC;

namespace Arbeidstider.Business.Logic.Repository
{
	public abstract class RepositoryBase
	{
	    protected internal static IDatabaseConnection Database
	    {
	        get { return Container.Resolve<IDatabaseConnection>();}
	    }
	}
}
