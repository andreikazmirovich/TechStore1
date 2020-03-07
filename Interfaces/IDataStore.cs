using System.Collections.Generic;
using System.IO;

namespace TechStore
{
    public interface IDataStore
    {
        string FullPath { get; }
        void SaveItem(Device device);
        List<Device> GetStore();
        List<Device> GetStore(FileStream fileStream);
        Device GetDevice(int id);
        void UpdateDevice(Device updatedDevice);
        bool DeleteDevice(int id);
    }
}