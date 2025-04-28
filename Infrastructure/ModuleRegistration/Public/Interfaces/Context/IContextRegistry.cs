using System.Collections.Generic;
using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
	public interface IContextRegistry
	{
		IEnumerable<IServiceLocator> AllLocators { get; }
		void RegisterLocator(IServiceLocator locator);
	}
}