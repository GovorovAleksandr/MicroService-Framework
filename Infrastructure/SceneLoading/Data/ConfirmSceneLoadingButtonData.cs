using System;
using MonoReferencing.Public;
using UnityEngine.UI;

namespace SceneLoading.Data
{
	[Serializable]
	internal struct ConfirmSceneLoadingButtonData : IMonoReferenceData
	{
		public Button Button;
	}
}