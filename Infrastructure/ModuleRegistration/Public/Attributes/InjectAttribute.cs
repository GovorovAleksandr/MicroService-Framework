using System;

namespace ModuleRegistration.Public
{
	[AttributeUsage(AttributeTargets.Field)]
	public class InjectAttribute : Attribute {}
}