using System;
using ModuleRegistration.Utils;

namespace ModuleRegistration.Public
{
	public interface IExistingServiceRegistry
	{
		void AddAll(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInterfaces(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInstance(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
	}
}