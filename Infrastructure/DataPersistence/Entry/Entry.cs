using System;
using DataPersistence.Core;
using ModuleRegistration.Public;
using TypeFinding.Public;

namespace DataPersistence.Entry
{
    internal sealed class Entry : ILocalModuleEntry, IPrioritizedModuleEntry
    {
        public BindingScene BindingScenes => BindingScene.All;

        public int Priority => 200;
        
        public void InstallBindings(IServiceLocator serviceLocator)
        {
            if (!ChildTypeFinder.TryGetChildTypes(typeof(IBaseFileNameBinder), out var fileNameBuilderTypes))
            {
                throw new Exception("Not found any file name builder types");
            }
            
            serviceLocator.AddAllFromNew(() => new DataRepository());
            serviceLocator.AddAllFromNew(() => new FilePreserver());
            serviceLocator.AddAllFromNew(() => new DefaultFilePersistence());
            serviceLocator.AddAllFromNew(() => new JsonDataSerializer());
            
            foreach (var fileNameBuilderType in fileNameBuilderTypes)
            {
                serviceLocator.AddAllFromNew(fileNameBuilderType, () => Activator.CreateInstance(fileNameBuilderType), ServiceBindingFlags.Multiple);
            }
            
            serviceLocator.AddAllFromNew(() => new LoadHandler());
        }
    }
}