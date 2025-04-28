using UnityEngine;

namespace MonoReferencing.Project.Scripts.Infrastructure.MonoReferencing.Configs
{
	//[CreateAssetMenu(menuName = "Project/Infrastructure/MonoReferencingConfig", fileName = "MonoReferencingConfig")]
	internal sealed class Config : ScriptableObject
	{
		[SerializeField] private bool _includeInactiveReferences;
		
		public bool IncludeInactiveReferences => _includeInactiveReferences;
	}
}