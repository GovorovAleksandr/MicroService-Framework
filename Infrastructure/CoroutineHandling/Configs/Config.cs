using CoroutineHandling.Core;
using UnityEngine;

namespace CoroutineHandling.Configs
{
	[CreateAssetMenu(menuName = "Project/Infrastructure/All/CoroutineHandlingConfig", fileName = "CoroutineHandlingConfig")]
	internal sealed class Config : ScriptableObject
	{
		[SerializeField] private CoroutineHandler _coroutineHandlerPrefab;
		
		public CoroutineHandler CoroutineHandlerPrefab => _coroutineHandlerPrefab;
	}
}