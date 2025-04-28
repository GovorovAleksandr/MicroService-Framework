using System;

namespace ModuleRegistration.Public
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class IgnoredByServiceLocatorAttribute : Attribute {}
}