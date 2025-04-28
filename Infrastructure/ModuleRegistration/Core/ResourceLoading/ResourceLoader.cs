using System;
using ModuleRegistration.Public;
using UnityEngine;

namespace ModuleRegistration.Core
{
    internal sealed class ResourceLoader : IResourceLoader, IDisposable
    {
        private const string Path = "Configs/";
        
        private readonly IServiceLocator _serviceLocator;

        public ResourceLoader(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public void Load()
        {
            var resources = Resources.LoadAll(Path);
            
            foreach (var resource in resources)
            {
                var type = resource.GetType();
                if (_serviceLocator.TryGet(type, out var _)) continue;
                _serviceLocator.AddAll(resource);
            }
            
            Resources.UnloadUnusedAssets();
        }

        public void Dispose()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}