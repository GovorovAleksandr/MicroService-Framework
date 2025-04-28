using System.Linq;
using EventBus.Events;
using EventBus.Public;
using ModuleRegistration.Core;
using ModuleRegistration.Public;
using IList = System.Collections.IList;

namespace EventBus.Core
{
    [MultipleContext]
    internal sealed class EventBus : IEventBus
    {
        [Inject] private readonly IContextRegistry _contextRegistry;

        public void Send<T>() where T : struct, IEvent
        {
            var eventData = (T)default;
            Send(eventData);
        }

        public void Send<T>(T eventData) where T : struct, IEvent
        {
            foreach (var locator in _contextRegistry.AllLocators)
            {
                if (!locator.TryGet(typeof(IEventHandler<T>), out var handlers)) continue;
                
                var orderedHandlers = ((IList)handlers)
                    .Cast<IEventHandler<T>>()
                    .OrderBy(handler => handler is IPrioritizedEventHandler<T> prioritizedEventHandler ? prioritizedEventHandler.EventHandlerPriority : 0)
                    .ToList();
            
                foreach (var handler in orderedHandlers)
                {
                    handler.Handle(eventData);
                }
            }
        }
    }
}