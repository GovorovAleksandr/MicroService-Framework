using System;
using ModuleRegistration.Public;
using UnityEngine.SceneManagement;

namespace ModuleRegistration.Core
{
    internal sealed class LocalContext : ContextBase<ILocalModuleEntry>
    {
        private IServiceLocatorInternal _globalServiceLocator;
        private IContextRegistryInternal _contextRegistry;
        
        protected override void PreInitialize()
        {
            var globalContextProvider = new GlobalContextProvider();
            globalContextProvider.Initialize();
            _globalServiceLocator = globalContextProvider.GlobalContext.ServiceLocator;

            if (_globalServiceLocator.TryGet<IContextRegistryInternal>(out _)) return;
            
            _contextRegistry = new ContextRegistry();
            _contextRegistry.RegisterLocator(_globalServiceLocator);
            _globalServiceLocator.AddInterfaces(_contextRegistry);
        }

        protected override bool CanBind(ILocalModuleEntry entry)
        {
            var bindingScenes = entry.BindingScenes;

            if (bindingScenes.HasFlag(BindingScene.All)) return true;
            if (!TryGetActiveSceneNameAsBindingScene(out var bindingScene)) return false;
            
            return bindingScenes.HasFlag(bindingScene);
        }

        protected override IServiceLocatorInternal CreateServiceLocator()
        {
            var serviceLocator = new ServiceLocator(_globalServiceLocator);
            _globalServiceLocator.Get<IContextRegistryInternal>().RegisterLocator(serviceLocator);
            return serviceLocator;
        }
        
        private void Start()
        {
            BootSequence.InitializeAll(_globalServiceLocator, ServiceLocator);
        }

        private static bool TryGetActiveSceneNameAsBindingScene(out BindingScene bindingScene) =>
            Enum.TryParse(GetActiveSceneName(), out bindingScene);
        
        private static string GetActiveSceneName() => SceneManager.GetActiveScene().name;
    }
}