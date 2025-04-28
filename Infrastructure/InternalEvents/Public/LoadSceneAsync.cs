using EventBus.Events;

namespace InternalEvents.Public
{
	public struct LoadSceneAsync : IEvent
	{
		public readonly string Name;
		public readonly bool AutoConfirm;

		public LoadSceneAsync(string name, bool autoConfirm) : this()
		{
			Name = name;
			AutoConfirm = autoConfirm;
		}
	}
}