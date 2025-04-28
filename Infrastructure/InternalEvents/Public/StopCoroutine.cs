using EventBus.Events;

namespace InternalEvents.Public
{
	public struct StopCoroutine : IEvent
	{
		public readonly int Id;

		public StopCoroutine(int id)
		{
			Id = id;
		}
	}
}