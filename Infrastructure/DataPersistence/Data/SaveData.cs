using System.Collections.Generic;

namespace DataPersistence.Data
{
    public struct SaveData
    {
        public Dictionary<string, object> Values;

        private SaveData(IDictionary<string, object> values)
        {
            Values = new Dictionary<string, object>(values);
        }

        public static SaveData GetDefault() => new SaveData(new Dictionary<string, object>());
    }
}