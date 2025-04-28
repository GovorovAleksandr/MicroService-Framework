using System;
using EventBus.Events;

namespace InternalEvents.Public
{
    public struct DataLoaded : IEvent
    {
        public readonly Type Type;
        public readonly object Data;

        public DataLoaded(Type type, object data)
        {
            Type = type;
            Data = data;
        }
    }
}