namespace RequestBus.Public
{
    public interface IRequestHandler<T> : IBaseRequestHandler where T : struct, IRequest
    {
        T Handle(T requestData);
    }
}