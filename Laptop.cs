using System;

namespace TechStore
{
    public class Laptop : Device
    {
        public string ProcessorName;
        public int RamMemoryGb;
        public MemoryType MemoryType;
        public float ScreenSize;

        public Laptop() {}

        public Laptop(string name, float price, string processorName, int ramMemoryGb, MemoryType memoryType, float screenSize) : base(name, price)
        {
            ProcessorName = processorName;
            RamMemoryGb = ramMemoryGb;
            MemoryType = memoryType;
            ScreenSize = screenSize;
        }

        public Laptop(string name, float price, int id, string processorName, int ramMemoryGb, MemoryType memoryType, float screenSize) : base(name, price, id)
        {
            ProcessorName = processorName;
            RamMemoryGb = ramMemoryGb;
            MemoryType = memoryType;
            ScreenSize = screenSize;
        }

        public override void FillProcess()
        {
            Console.Write("Processor name: ");
            ProcessorName = Console.ReadLine();

            Console.Write("RAM memory (GB): ");
            RamMemoryGb = Convert.ToInt32(Console.ReadLine());

            Console.Write("Memory type: ");
            MemoryType = Enum.Parse<MemoryType>(Console.ReadLine());

            Console.Write("Screen size: ");
            ScreenSize = Convert.ToSingle(Console.ReadLine());

            base.FillProcess();
        }
    }
}