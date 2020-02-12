using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TechStore
{
    public class DataStore : IDataStore
    {
        private readonly string _filePath;
        private readonly string _storeFile = "store.json";

        public string FullPath => _filePath + "/" + _storeFile;

        public DataStore(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(FullPath))
            {
                File.Create(FullPath);
            }
        }

        public void SaveItem<T>(T device) where T:IDevice
        {
            using (var fileStream = File.Open(FullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                var store = GetStore<T>(fileStream);
                device.Id = store.Count + 1;
                store.Add(device);
                UpdateStore(fileStream, store);
            }
        }

        public List<T> GetStore<T>()
        {
            using (var streamReader = new StreamReader(FullPath))
            {
                return FromJson<T>(streamReader.ReadToEnd()) ?? new List<T>();
            }
        }

        public List<T> GetStore<T>(FileStream fileStream)
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                return FromJson<T>(streamReader.ReadToEnd()) ?? new List<T>();
            }
        }

        private void UpdateStore(FileStream fileStream, List<IDevice> store) 
        {
            var jsonStore = ToJson(store);
            var streamWriter = new StreamWriter(fileStream);
            fileStream.SetLength(0);
            streamWriter.Write(jsonStore);
            streamWriter.Flush();
        }

        public IDevice GetDevice(int id)
        {
            using (var fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var store = GetStore<IDevice>(fileStream);
                return store.Find(device => device.Id == id);
            }
        }

        public void UpdateDevice(IDevice updatedDevice)
        {
            using (var fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var store = GetStore<IDevice>(fileStream);
                var indexOfExistingDevice = store.FindIndex(device => device.Id == updatedDevice.Id);
                if (indexOfExistingDevice > 0)
                {
                    store[indexOfExistingDevice] = updatedDevice;
                }

                UpdateStore(fileStream, store);
            }
        }

        private string ToJson(object objectToJson)
        {
            return JsonConvert.SerializeObject(objectToJson);
        }
        private List<T> FromJson<T>(string stringObject)
        {
            return JsonConvert.DeserializeObject<List<T>>(stringObject);
        }
    }
}