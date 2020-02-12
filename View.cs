using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            }
        }

        private void ShowAllDevices()
        {
            foreach (var device in DataStore.GetStore<Device>())
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
            Device newDevice = null;
            switch ((AddMenuEnum)Convert.ToInt32(Console.ReadLine()))
            {
                case AddMenuEnum.Laptop:
                    newDevice = new Laptop();
                    break;
                case AddMenuEnum.Smartphone:
                    newDevice = new Smartphone();
                    break;
            }

            if (newDevice != null)
            {
                newDevice.FillProcess();
                DataStore.SaveItem(newDevice);
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
    }
}