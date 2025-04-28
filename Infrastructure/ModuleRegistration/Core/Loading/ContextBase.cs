using System;
using System.Linq;
using ModuleRegistration.Public;
using TypeFinding.Public;
using UnityEngine;

namespace ModuleRegistration.Core
{
    internal abstract class ContextBase<T> : MonoBehaviour where T : class, IModuleEntry
    {
        internal IServiceLocatorInternal ServiceLocator { get; private set; }
        
        protected virtual bool CanBind(T installer) => true;
        protected virtual void PreInitialize() {}
        protected abstract IServiceLocatorInternal CreateServiceLocator();
        
        private void Awake()
        {
            PreInitialize();
            
            if (!ChildTypeFinder.TryGetChildTypes(typeof(T), out var childTypes)) return;
            
            ServiceLocator = CreateServiceLocator();
            
            var entries = childTypes.Select(type => Activator.CreateInstance(type) as T).ToList();
            
            var sortedEntries = entries.OrderBy(entry =>
            {
                var type = entry.GetType();
                if (type.GetInterface(nameof(IPrioritizedModuleEntry)) == null) return 0;
                
                var prioritizedInstaller = (IPrioritizedModuleEntry)entry;
                
                return prioritizedInstaller.Priority;
            }).ToList();
            
            foreach (var entry in sortedEntries)
            {
                if (!CanBind(entry)) continue;
                
                entry.InstallBindings(ServiceLocator);
            }
        }

        private void OnDestroy()
        {
            ServiceLocator.DisposeAll();
            ServiceLocator.Get<IContextRegistryInternal>().TryRemoveLocator(ServiceLocator);
        }
    }
}