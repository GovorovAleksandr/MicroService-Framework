using System;
using System.Collections.Generic;
using ModuleRegistration.Public;

namespace ModuleRegistration.Utils
{
	internal sealed class Constants
	{
		public static readonly IEnumerable<Type> IgnoredByServiceLocatorTypes = new List<Type>()
		{
			typeof(IDisposable)
		};

		public const ServiceBindingFlags DefaultServiceBindingFlag = ServiceBindingFlags.Single;
	}
}