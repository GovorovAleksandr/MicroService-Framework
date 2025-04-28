using System.Collections;
using RequestBus.Public;

namespace InternalRequests.Public
{
	public struct StartCoroutine : IRequest
	{ 
		public readonly IEnumerator Routine;

		public readonly int Id;

		public StartCoroutine(IEnumerator routine) : this()
		{
			Routine = routine;
		}

		public StartCoroutine(int id) : this()
		{
			Id = id;
		}
	}
}