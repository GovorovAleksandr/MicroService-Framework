using System;
using MonoReferencing.Public;
using UnityEngine.UI;

namespace SceneLoading.Data
{
	[Serializable]
	internal struct LoadSceneButtonReferenceData : IMonoReferenceData
	{
		public Button Button;
		public string SceneName;
	}
}