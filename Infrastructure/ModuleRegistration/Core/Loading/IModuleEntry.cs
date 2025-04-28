using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
    public interface IModuleEntry
    {
        void InstallBindings(IServiceLocator serviceLocator);
    }
}