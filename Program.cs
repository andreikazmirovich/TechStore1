using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TechStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataStore = new DataStore("../../..");
            var view = new View(dataStore);
            view.InitMenu();
        }
    }
}
