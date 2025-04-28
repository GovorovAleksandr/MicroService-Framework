using EventBus.Events;

namespace InternalEvents.Public
{
	public struct LoadScene : IEvent
	{
		public enum SceneNames
		{
			Menu,
			Gameplay
		}
		
		public readonly string Name;

		public LoadScene(SceneNames name)
		{
			Name = name.ToString();
		}
	}
}