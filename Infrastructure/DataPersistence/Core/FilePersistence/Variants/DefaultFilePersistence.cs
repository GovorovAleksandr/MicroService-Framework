using System.IO;
using UnityEngine;

namespace DataPersistence.Core
{
    internal sealed class DefaultFilePersistence : IFilePersistence
    {
        private const string FullFileNameFormat = "{0}.{1}";
        private const string ExtensionName = "json";
        
        private static string Path => Application.persistentDataPath;

        public void Save(string fileName, string data) => File.WriteAllText(GetFullPath(fileName), data);

        public string Load(string fileName) =>
            File.Exists(GetFullPath(fileName)) ?
                File.ReadAllText(GetFullPath(fileName)) :
                string.Empty;
        
        private string GetFullPath(string fileName)
        {
            var fullFileName = string.Format(FullFileNameFormat, fileName, ExtensionName);
            return System.IO.Path.Combine(Path, fullFileName);
        }
    }
}