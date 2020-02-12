namespace TechStore
{
    public interface IDevice
    {
        int Id { get; set; }
        string Name { get; set; }
        float Price { get; set; }
        void FillProcess();
        void DeviceInfo();
    }
}