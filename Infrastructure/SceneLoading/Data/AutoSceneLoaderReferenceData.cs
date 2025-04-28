using System;
using MonoReferencing.Public;

namespace SceneLoading.Data
{
	[Serializable]
	internal struct AutoSceneLoaderReferenceData : IMonoReferenceData
	{
		public string SceneName;
		public bool AutoConfirm;
	}
}