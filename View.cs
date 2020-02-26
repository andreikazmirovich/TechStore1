using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace TechStore
{
    public class View
    {
        public IDataStore DataStore { get; }

        public View(IDataStore dataStore)
        {
            DataStore = dataStore;
        }

        public void InitMenu()
        {
            while (true)
            {
                MainMenu();
            }
        }

        private string ParseMenuItem<T>(string menuItem) where T : struct
        {
            var menuItemId = Convert.ToInt32(Enum.Parse<T>(menuItem));
            var menuItemName = String.Join(' ', Regex.Split(menuItem, @"(?<!^)(?=[A-Z])"));
            return menuItemId + ". " + menuItemName;
        }

        private void MainMenu()
        {
            Console.Clear();
            foreach (var menuItem in Enum.GetNames(typeof(MainMenuEnum)))
            {
                Console.WriteLine(ParseMenuItem<MainMenuEnum>(menuItem));
            }

            Console.Write("Choose menu item: ");
            switch ((MainMenuEnum)Convert.ToInt32(Console.ReadLine()))
            {
                case MainMenuEnum.AllDevices:
                    ShowAllDevices();
                    break;
                case MainMenuEnum.AddDevice:
                    AddDevice();
                    break;
                case MainMenuEnum.EditDevice:
                    UpdateDevice();
                    break;
                case MainMenuEnum.DeleteDevice:
                    DeleteDevice();
                    break;

                case MainMenuEnum.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        private void ShowAllDevices()
        {
            Console.Clear();
            foreach (var device in DataStore.GetStore())
            {
                device.DeviceInfo();
            }
            ShowExit();
        }

        private void AddDevice()
        {
            Console.Clear();
            foreach (var menuItem in Enum.GetNames(typeof(AddMenuEnum)))
            {
                Console.WriteLine(ParseMenuItem<AddMenuEnum>(menuItem));
            }

            Console.Write("Choose device: ");
            var newBaseDevice = new Device();

            newBaseDevice.FillProcess();
            DataStore.SaveItem(newBaseDevice);
            ShowCompleteMessage();
        }

        private void UpdateDevice()
        {
            Console.Clear();
            Console.Write("ID: ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out int id))
                {
                    Console.Clear();
                    var device = DataStore.GetDevice(id);
                    device.DeviceInfo();

                    Console.WriteLine("What to change?");
                    var propertyToChange = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(propertyToChange))
                    {
                        var existedDetail = device.TechnicalDetails.Find(detail => detail.Name.ToLower() == propertyToChange.ToLower());
                        if (existedDetail != null)
                        {
                            Console.Write("Value: ");
                            existedDetail.Value = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Value: ");
                            var newTechnicalDetailValue = Console.ReadLine();
                            device.TechnicalDetails.Add(new TechnicalDetail(propertyToChange, newTechnicalDetailValue));
                        }
                        DataStore.UpdateDevice(device);
                        ShowCompleteMessage();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
            }
        }

        private void DeleteDevice()
        {
            Console.Clear();
            Console.Write("ID:");
            if (Int32.TryParse(Console.ReadLine(), out int id))
            {
                if (DataStore.DeleteDevice(id)) {
                    ShowCompleteMessage();
                }
                else
                {
                    Console.WriteLine("Something went wrong :c");
                };
            }
        }

        private void ShowExit()
        {
            Console.WriteLine("\nPress Backspace to go back");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Backspace)
                {
                    break;
                }
            }
        }

        private void ShowCompleteMessage()
        {
            Console.WriteLine("Done!");
            Thread.Sleep(1000);
        }
    }
}