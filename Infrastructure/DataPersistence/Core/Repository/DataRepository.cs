using System.Collections.Generic;
using DataPersistence.Data;
using InternalEvents.Public;

namespace DataPersistence.Core
{
	internal sealed class DataRepository : IDataRepository
	{
		private readonly Dictionary<string, SaveData> _data = new Dictionary<string, SaveData>();
		
		public SaveData GetFileData(string fileName)
		{
			return !_data.TryGetValue(fileName, out var data) ? SaveData.GetDefault() : data;
		}

		public void OverrideFileData(string fileName, SaveData data)
		{
			_data[fileName] = data;
		}

		public void OverrideData(string fileName, ISavableData data)
		{
			var dataType = data.GetType();
			var typeName = dataType.Name;
			var fileData = GetFileData(fileName);
			
			_data[fileName] = fileData;
			fileData.Values[typeName] = data;
		}
	}
}