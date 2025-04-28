using System.Collections.Generic;
using ModuleRegistration.Core;

namespace ModuleRegistration.Public
{
	internal sealed class ContextRegistry : IContextRegistry
	{
		private readonly List<IServiceLocator> _locators = new List<IServiceLocator>();

		public IEnumerable<IServiceLocator> AllLocators => _locators;
		
		public void RegisterLocator(IServiceLocator locator)
		{
			if (_locators.Contains(locator)) return;
			_locators.Add(locator);
		}
	}
}