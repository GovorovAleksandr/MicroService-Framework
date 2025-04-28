using System;
using ModuleRegistration.Core;
using ModuleRegistration.Public;
using RequestBus.Public;

namespace RequestBus.Core
{
    [MultipleContext]
    internal sealed class RequestBus : IRequestBus
    {
        [Inject] private readonly IContextRegistry _contextRegistry;

        public T Send<T>() where T : struct, IRequest
        {
            var requestData = (T)default;
            return Send(requestData);
        }

        public T Send<T>(T requestData) where T : struct, IRequest
        {
            foreach (var locator in _contextRegistry.AllLocators)
            {
                if (!locator.TryGet<IRequestHandler<T>>(out var handler)) continue;
                return handler.Handle(requestData);
            }
            
            throw new InvalidOperationException($"No handler registered for request type {typeof(T).Name}.");
        }
    }
}