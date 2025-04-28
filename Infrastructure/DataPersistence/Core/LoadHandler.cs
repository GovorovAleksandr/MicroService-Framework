using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using TypeFinding.Public;

namespace DataPersistence.Core
{
	internal sealed class LoadHandler : IInitializable
	{
		[Inject] private readonly IEventBus _eventBus;
		[Inject] private readonly IDataRepository _dataRepository;
		[Inject] private readonly IDataSerializer _dataSerializer;
		[Inject] private readonly IFilePersistence _filePersistence;
		[Inject] private readonly List<IBaseFileNameBinder> _fileNameBinders;
		
		public void Initialize()
		{
			if (!ChildTypeFinder.TryGetChildTypes(typeof(ISavableData),
				    out var savableDataTypes)) return;
			
			var loadedDataTypes = new List<Type>();
			
			var fileNames = _fileNameBinders.Select(binder => binder.FileName);
			
			foreach (var fileName in fileNames)
			{
				var serializedData = _filePersistence.Load(fileName);
				var loadedData = _dataSerializer.Deserialize(serializedData);
                
				_dataRepository.OverrideFileData(fileName, loadedData);

				foreach (var data in loadedData.Values.Values)
				{
					var dataType = data.GetType();
					_eventBus.Send(new DataLoaded(dataType, data));
					loadedDataTypes.Add(dataType);
				}
			}

			foreach (var dataType in savableDataTypes)
			{
				if (loadedDataTypes.Contains(dataType)) continue;
				_eventBus.Send(new DataNotLoaded(dataType));
			}
		}
	}
}