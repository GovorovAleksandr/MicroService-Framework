using DataPersistence.Core.Preserving;
using ModuleRegistration.Public;

namespace DataPersistence.Core
{
	internal sealed class FilePreserver : IFilePreserver
	{
		[Inject] private readonly IDataRepository _dataRepository;
		[Inject] private readonly IDataSerializer _dataSerializer;
		[Inject] private readonly IFilePersistence _filePersistence;

		public void Preserve(string fileName)
		{
			var data = _dataRepository.GetFileData(fileName);
            
			var serializedData = _dataSerializer.Serialize(data);
			_filePersistence.Save(fileName, serializedData);
		}
	}
}