using System.Collections.Generic;

namespace ModuleRegistration.Public
{
	internal sealed class ContextRegistry : IContextRegistryInternal
	{
		private readonly List<IServiceLocator> _locators = new List<IServiceLocator>();

		public IEnumerable<IServiceLocator> AllLocators => _locators;
		
		public void RegisterLocator(IServiceLocator locator)
		{
			if (_locators.Contains(locator)) return;
			_locators.Add(locator);
		}

		public bool TryRemoveLocator(IServiceLocator locator)
		{
			if (!_locators.Contains(locator)) return false;

			_locators.Remove(locator);
			
			return true;
		}
	}
}