using CoroutineHandling.Configs;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;

namespace CoroutineHandling.Core
{
	internal sealed class EventHandler : IEventHandler<StartCoroutine>, IEventHandler<StopCoroutine>
	{
		[Inject] private readonly IEventBus _eventBus;
		[Inject] private readonly ICoroutineHandlerProvider _coroutineHandlerProvider;
		[Inject] private readonly ICoroutineRegistry _coroutineRegistry;
		[Inject] private readonly Config _config;

		public void Handle(StartCoroutine eventData)
		{
			var routine = eventData.Routine;
			
			_coroutineHandlerProvider.Get().StartCoroutine(routine);
		}

		public void Handle(StopCoroutine eventData)
		{
			var id = eventData.Id;
			
			var coroutine = _coroutineRegistry.Get(id);
			
			_coroutineHandlerProvider.Get().StopCoroutine(coroutine);
		}
	}
}