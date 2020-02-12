using System.Collections.Generic;
using System.IO;

namespace TechStore
{
    public interface IDataStore
    {
        string FullPath { get; }
        void SaveItem(IDevice device);
        List<T> GetStore<T>();
        List<T> GetStore<T>(FileStream fileStream);
        IDevice GetDevice(int id);
        void UpdateDevice(IDevice updatedDevice);
    }
}