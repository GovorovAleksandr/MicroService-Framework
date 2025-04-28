using System.Collections;
using EventBus.Events;

namespace InternalEvents.Public
{
	public struct StartCoroutine : IEvent
	{
		public readonly IEnumerator Routine;

		public StartCoroutine(IEnumerator routine)
		{
			Routine = routine;
		}
	}
}