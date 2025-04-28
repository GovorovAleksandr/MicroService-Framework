using System;
using MonoReferencing.Public;
using UnityEngine.UI;

namespace SceneLoading.Data
{
	[Serializable]
	internal struct ProgressSliderReferenceData : IMonoReferenceData
	{
		public Slider Slider;
	}
}