using System.Collections.Generic;
using UnityEngine;

namespace CoroutineHandling.Core
{
	internal sealed class CoroutineRegistry : ICoroutineRegistry
	{
		private readonly Dictionary<int, Coroutine> _coroutines = new Dictionary<int, Coroutine>();
		
		public void Add(int id, Coroutine coroutine)
		{
			_coroutines.Add(id, coroutine);
		}

		public Coroutine Get(int id)
		{
			var coroutine =  _coroutines[id];
			_coroutines.Remove(id);
			return coroutine;
		}
	}
}