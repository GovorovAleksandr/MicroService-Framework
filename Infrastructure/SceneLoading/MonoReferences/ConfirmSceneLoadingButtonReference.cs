using MonoReferencing.Public;

namespace SceneLoading.Data
{
	internal sealed class ConfirmSceneLoadingButtonReference : MonoReference<ConfirmSceneLoadingButtonData>
	{
		public override bool AllowMultiple => true;
	}
}