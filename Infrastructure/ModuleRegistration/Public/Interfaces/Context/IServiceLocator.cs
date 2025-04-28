using System;

namespace ModuleRegistration.Public
{
	[MultipleContext]
	public interface IServiceLocator : IServiceRegistry, INewServiceRegistry, IExistingServiceRegistry
	{
		T Get<T>();
		object Get(Type type);
		
		bool TryGet<T>(out T service);
		bool TryGet(Type serviceType, out object service);
	}
}