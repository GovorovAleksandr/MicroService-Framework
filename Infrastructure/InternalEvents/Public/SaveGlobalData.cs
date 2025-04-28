using EventBus.Events;

namespace InternalEvents.Public
{
	public struct SaveGlobalData : ISaveEvent
	{
		public SaveGlobalData(ISavableData data)
		{
			Data = data;
		}

		public ISavableData Data { get; }
	}
	
	public interface ISaveEvent : IEvent
	{
		ISavableData Data { get; }
	}
	
	public interface ISavableData {}
}