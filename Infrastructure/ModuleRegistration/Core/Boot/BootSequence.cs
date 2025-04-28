using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
	internal static class BootSequence
	{
		public static void InitializeAll(IServiceLocatorInternal global, IServiceLocatorInternal local)
		{
			IResourceLoader resourceLoader = new ResourceLoader(global);
			resourceLoader.Load();
			
			global.Get<IServiceInjectorInternal>().InjectAll();
			local.Get<IServiceInjectorInternal>().InjectAll();

			global.InitializeAll();
			local.InitializeAll();
		}
	}
}