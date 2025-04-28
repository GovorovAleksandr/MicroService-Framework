namespace ModuleRegistration.Public
{
	[ForceMultiple, MultipleContext]
	public interface IInitializable
	{
		void Initialize();
	}
}