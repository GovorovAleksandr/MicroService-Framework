namespace ModuleRegistration.Public
{
    [MultipleContext]
    public interface IServiceInjector
    {
        void Inject(object service);
    }
}