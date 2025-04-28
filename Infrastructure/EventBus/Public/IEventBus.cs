using EventBus.Events;

namespace EventBus.Public
{
    public interface IEventBus
    {
        void Send<T>() where T : struct, IEvent;
        void Send<T>(T eventData) where T : struct, IEvent;
    }
}