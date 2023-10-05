using LendingSystemUI.Core;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace LendingSystemUI.Services
{
    public interface IDataService
    {
        void SaveData<T>(string filePath, ObservableCollection<T> models);
        ObservableCollection<T> LoadData<T>(string filePath);
        string GetFilePath(string fileName);
    }

    public class XmlDataService : BaseViewModel, IDataService
    {
        private string _dataPath;
        public string DataPath
        {
            get => _dataPath;
            private set
            {
                _dataPath = value;
                OnPropertyChanged(nameof(DataPath));
            }
        }

        public XmlDataService()
        {
            DataPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Data"));

            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
        }

        public void SaveData<T>(string filePath, ObservableCollection<T> models)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, models);
            }
        }

        public ObservableCollection<T> LoadData<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                return (ObservableCollection<T>)serializer.Deserialize(reader);
            }
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(DataPath, $"{fileName}.xml");
        }
    }

    public class SqlDataService : BaseViewModel, IDataService
    {
        public void SaveData<T>(string connectionString, ObservableCollection<T> models)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> LoadData<T>(string connectionString)
        {
            throw new NotImplementedException();
        }

        public string GetFilePath(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}


