using CoroutineHandling.Configs;
using ModuleRegistration.Public;
using RequestBus.Public;
using StartCoroutine = InternalRequests.Public.StartCoroutine;

namespace CoroutineHandling.Core
{
	internal sealed class RequestHandler : IRequestHandler<StartCoroutine>
	{
		[Inject] private readonly IRequestBus _requestBus;
		[Inject] private readonly ICoroutineHandlerProvider _coroutineHandlerProvider;
		[Inject] private readonly ICoroutineRegistry _coroutineRegistry;
		[Inject] private readonly Config _config;

		public StartCoroutine Handle(StartCoroutine requestData)
		{
			var routine = requestData.Routine;
			
			var coroutine = _coroutineHandlerProvider.Get().StartCoroutine(routine);

			var id = coroutine.GetHashCode();

			_coroutineRegistry.Add(id, coroutine);
			
			return new StartCoroutine(id);
		}
	}
}