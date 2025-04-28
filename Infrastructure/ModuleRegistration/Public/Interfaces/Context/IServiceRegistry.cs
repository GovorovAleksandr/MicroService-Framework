using System;
using ModuleRegistration.Utils;

namespace ModuleRegistration.Public
{
	public interface IServiceRegistry
	{
		void AddAs<TKey>(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
		void AddAs(Type keyType, object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag);
	}
}