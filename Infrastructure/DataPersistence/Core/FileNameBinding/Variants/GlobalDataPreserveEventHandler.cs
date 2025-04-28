using DataPersistence.Core.Preserving;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;

namespace DataPersistence.Core
{
	internal sealed class GlobalDataPreserveEventHandler : IBaseFileNameBinder, IEventHandler<SaveGlobalData>
	{
		public string FileName => "GlobalData";
		
		[Inject] private readonly IEventBus _eventBus;
		[Inject] private readonly IDataRepository _dataRepository;
		[Inject] private readonly IFilePreserver _filePreserver;

		public void Handle(SaveGlobalData eventData)
		{
			var data = eventData.Data;
            
			_dataRepository.OverrideData(FileName, data);
			_filePreserver.Preserve(FileName);
		}
	}
}