using ModuleRegistration.Public;
using MonoReferencing.Core;

namespace MonoReferencing.Installers
{
    internal sealed class Entry : ILocalModuleEntry, IPrioritizedModuleEntry
    {
        public BindingScene BindingScenes => BindingScene.All;

        public int Priority => 300;

        public void InstallBindings(IServiceLocator serviceLocator)
        {
            serviceLocator.AddInterfacesFromNew<ReferenceInstaller>();
        }
    }
}