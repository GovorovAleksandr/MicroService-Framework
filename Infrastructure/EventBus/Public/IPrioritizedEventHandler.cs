using EventBus.Events;

namespace EventBus.Public
{
	public interface IPrioritizedEventHandler<T> : IEventHandler<T> where T : struct, IEvent
	{
		int EventHandlerPriority { get; }
	}
}