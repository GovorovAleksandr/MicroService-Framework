using System;
using System.Collections.Generic;
using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
	internal interface IServiceLocatorInternal : IServiceLocator, IServiceInitializer, IServiceDisposer
	{
		IDictionary<Type, ServiceContainer> Services { get; }
		IDictionary<Type, ServiceContainer> AllServices { get; }
	}
}