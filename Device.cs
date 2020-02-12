using System;
using Newtonsoft.Json;

namespace TechStore
{
    public class Device : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public Device() {}

        public Device(string name, float price)
        {
            Name = name;
            Price = price;
        }

        [JsonConstructor]
        public Device(string name, float price, int id) : this(name, price)
        {
            Id = id;
        }

        public virtual void FillProcess()
        {
            Console.Write("Name: ");
            Name = Console.ReadLine();

            Console.Write("Price: ");
            Price = Convert.ToSingle(Console.ReadLine());

            Console.WriteLine("Done!");
        }

        public void DeviceInfo()
        {
            Console.WriteLine("№{0}", Id);
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Price: ${0}", Price);
            Console.WriteLine("-------------------------");
            Console.WriteLine("");
        }
    }
}