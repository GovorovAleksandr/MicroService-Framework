namespace RequestBus.Public
{
    public interface IRequestBus
    {
        T Send<T>() where T : struct, IRequest;
        T Send<T>(T requestData) where T : struct, IRequest;
    }
}