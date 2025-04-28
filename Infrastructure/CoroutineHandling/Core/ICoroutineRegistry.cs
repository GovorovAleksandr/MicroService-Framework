using UnityEngine;

namespace CoroutineHandling.Core
{
	internal interface ICoroutineRegistry
	{
		void Add(int id, Coroutine coroutine);
		Coroutine Get(int id);
	}
}