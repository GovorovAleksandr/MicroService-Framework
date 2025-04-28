using EventBus.Events;

namespace InternalEvents.Public
{
	public struct SceneLoadingProgressChanged : IEvent
	{
		public readonly float Progress;

		public SceneLoadingProgressChanged(float progress)
		{
			Progress = progress;
		}
	}
}