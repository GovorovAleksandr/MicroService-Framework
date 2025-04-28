namespace DataPersistence.Core
{
    internal interface IFilePersistence
    {
        void Save(string fileName, string data);
        string Load(string fileName);
    }
}