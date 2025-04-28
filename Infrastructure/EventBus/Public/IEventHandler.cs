using EventBus.Events;
using ModuleRegistration.Public;

namespace EventBus.Public
{
    [MultipleContext, ForceMultiple]
    public interface IEventHandler<T> : IBaseEventHandler where T : IEvent
    {
        void Handle(T eventData);
    }
}