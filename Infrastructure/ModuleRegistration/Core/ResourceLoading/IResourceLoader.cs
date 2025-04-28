using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
	[MultipleContext]
	internal interface IResourceLoader
	{
		void Load();
	}
}