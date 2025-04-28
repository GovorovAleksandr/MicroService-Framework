using System;
using EventBus.Events;

namespace InternalEvents.Public
{
	public struct DataNotLoaded : IEvent
	{
		public readonly Type Type;

		public DataNotLoaded(Type type)
		{
			Type = type;
		}
	}
}