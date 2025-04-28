using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
    internal sealed class GlobalContext : ContextBase<IGlobalModuleEntry>
    {
        protected override IServiceLocatorInternal CreateServiceLocator() => new ServiceLocator();
    }
}