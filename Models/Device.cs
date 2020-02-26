using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TechStore
{
    public class Device : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<TechnicalDetail> TechnicalDetails = new List<TechnicalDetail>();

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

        public void FillProcess()
        {
            Console.Clear();
            Console.Write("Name: ");
            Name = Console.ReadLine();

            Console.Write("Price: ");
            Price = Convert.ToSingle(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("Do you want to add additional properties? (y/n)");
                var key = Console.ReadLine();
                if (key == "y" || key == "Y")
                {
                    Console.Clear();
                    Console.Write("Property: ");
                    var propertyName = Console.ReadLine();
                    Console.Write("Value: ");
                    var propertyValue = Console.ReadLine();

                    TechnicalDetails.Add(new TechnicalDetail(propertyName, propertyValue));
                }
                else if (key == "n" || key == "N")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter correct answer");
                }
            }

            Console.WriteLine("Done!");
        }

        public void DeviceInfo()
        {
            Console.WriteLine("#" + Id);
            Console.WriteLine(Name);

            foreach (var technicalDetail in TechnicalDetails)
            {
                Console.WriteLine("{0}: {1}", technicalDetail.Name, technicalDetail.Value);
            }

            Console.WriteLine("----------");
            Console.WriteLine("$" + Price);
            Console.WriteLine();
        }
    }
}