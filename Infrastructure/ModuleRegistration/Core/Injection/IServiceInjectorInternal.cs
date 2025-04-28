namespace ModuleRegistration.Public
{
	[MultipleContext]
	public interface IServiceInjectorInternal : IServiceInjector
	{
		void InjectAll();
	}
}