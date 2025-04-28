using System;
using EventBus.Events;

namespace InternalEvents.Public
{
	public struct SceneLoaded : IEvent
	{
		public readonly Action Confirm;

		public SceneLoaded(Action confirm)
		{
			Confirm = confirm;
		}
	}
}