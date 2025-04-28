using MonoReferencing.Public;
using SceneLoading.Data;

namespace SceneLoading.MonoReferences
{
	internal sealed class LoadSceneButtonReference : MonoReference<LoadSceneButtonReferenceData>
	{
		public override bool AllowMultiple => true;
	}
}