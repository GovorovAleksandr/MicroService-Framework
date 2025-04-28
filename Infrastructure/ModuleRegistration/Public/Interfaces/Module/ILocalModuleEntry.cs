using ModuleRegistration.Core;

namespace ModuleRegistration.Public
{
    public interface ILocalModuleEntry : IModuleEntry
    {
        BindingScene BindingScenes { get; }
    }
}