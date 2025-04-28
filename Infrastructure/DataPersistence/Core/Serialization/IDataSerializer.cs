using DataPersistence.Data;

namespace DataPersistence.Core
{
    internal interface IDataSerializer
    {
        string Serialize(SaveData data);
        SaveData Deserialize(string data);
    }
}