using System;
using ModuleRegistration.Utils;

namespace ModuleRegistration.Public
{
	public interface INewServiceRegistry
	{
		void AddAllFromNew<T>(ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddAllFromNew(Type type, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);

		void AddInterfacesFromNew<T>(ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInterfacesFromNew(Type type, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);

		void AddInstanceFromNew<T>(ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInstanceFromNew(Type type, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
	}
}