using DataPersistence.Data;
using InternalEvents.Public;

namespace DataPersistence.Core
{
	internal interface IDataRepository
	{
		SaveData GetFileData(string fileName);
		void OverrideData(string fileName, ISavableData data);
		void OverrideFileData(string fileName, SaveData data);
	}
}