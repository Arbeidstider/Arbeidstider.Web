using Autofac;

namespace Arbeidstider.Business.Interfaces
{
    public interface IContainerAccessor
    {
        IContainer Container { get; set; }
    }
}
