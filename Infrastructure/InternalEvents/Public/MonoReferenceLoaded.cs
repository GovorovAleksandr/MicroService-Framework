using System;
using EventBus.Events;

namespace InternalEvents.Public
{
	public struct MonoReferenceLoaded : IEvent
	{
		public readonly Type Type;
		public readonly object Reference;

		public MonoReferenceLoaded(Type type, object reference)
		{
			Type = type;
			Reference = reference;
		}
	}
}