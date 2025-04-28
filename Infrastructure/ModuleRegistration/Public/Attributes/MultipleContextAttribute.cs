using System;

namespace ModuleRegistration.Public
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)]
	public class MultipleContextAttribute : Attribute {}
}