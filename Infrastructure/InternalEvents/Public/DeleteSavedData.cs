using System;
using EventBus.Events;

namespace InternalEvents.Public
{
    public struct DeleteSavedData : IEvent
    {
        public readonly Type DataType;

        public DeleteSavedData(Type dataType)
        {
            DataType = dataType;
        }
    }
}