using System;

namespace ModuleRegistration.Public
{
	[AttributeUsage(AttributeTargets.Interface)]
	public sealed class ForceMultiple : Attribute{}
}