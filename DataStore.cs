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

        public void SaveItem(Device device)
        {
            using (var fileStream = File.Open(FullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var store = GetStore(fileStream);
                device.Id = store.Count + 1;
                store.Add(device);
                UpdateStore(fileStream, store);
            }
        }

        public List<Device> GetStore()
        {
            using (var streamReader = new StreamReader(FullPath))
            {
                return FromJson(streamReader.ReadToEnd()) ?? new List<Device>();
            }
        }

        public List<Device> GetStore(FileStream fileStream)
        {
            var streamReader = new StreamReader(fileStream);
            return FromJson(streamReader.ReadToEnd()) ?? new List<Device>();
        }

        private void UpdateStore(FileStream fileStream, List<Device> store) 
        {
            var jsonStore = ToJson(store);
            var streamWriter = new StreamWriter(fileStream);
            fileStream.SetLength(0);
            streamWriter.Write(jsonStore);
            streamWriter.Flush();
        }

        public Device GetDevice(int id)
        {
            using (var fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var store = GetStore(fileStream);
                return store.Find(device => device.Id == id);
            }
        }

        public void UpdateDevice(Device updatedDevice)
        {
            using (var fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var store = GetStore(fileStream);
                var indexOfExistingDevice = store.FindIndex(device => device.Id == updatedDevice.Id);
                if (indexOfExistingDevice >= 0)
                {
                    store[indexOfExistingDevice] = updatedDevice;
                }

                UpdateStore(fileStream, store);
            }
        }

        public bool DeleteDevice(int id)
        {
            using (var fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var store = GetStore(fileStream);
                var indexOfExistingDevice = store.FindIndex(device => device.Id == id);
                if (indexOfExistingDevice >= 0)
                {
                    store.RemoveAt(indexOfExistingDevice);
                    UpdateStore(fileStream, store);
                    return true;
                }

                return false;
            }
        }

        private string ToJson(object objectToJson)
        {
            return JsonConvert.SerializeObject(objectToJson);
        }
        private List<Device> FromJson(string stringObject)
        {
            return JsonConvert.DeserializeObject<List<Device>>(stringObject);
        }
    }
}