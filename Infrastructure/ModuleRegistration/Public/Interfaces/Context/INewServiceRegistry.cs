using System;
using ModuleRegistration.Utils;

namespace ModuleRegistration.Public
{
	public interface INewServiceRegistry
	{
		void AddAllFromNew<T>(Func<T> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddAllFromNew(Type type, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);

		void AddInterfacesFromNew<T>(Func<T> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInterfacesFromNew(Type type, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);

		void AddInstanceFromNew<T>(Func<T> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddInstanceFromNew(Type type, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		
		void AddAsFromNew<TKey, TInstance>(Func<TInstance> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddAsFromNew(Type keyType, Type serviceType, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
	}
}