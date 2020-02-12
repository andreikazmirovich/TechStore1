using System;

namespace TechStore
{
    public class Smartphone: Device
    {
        public float ScreenSize;
        public OperationSystems OS;
        public float CameraPixels;

        public Smartphone() {}

        public Smartphone(string name, float price, float screenSize, OperationSystems os, float cameraPixels) : base(name, price)
        {
            ScreenSize = screenSize;
            OS = os;
            CameraPixels = cameraPixels;
        }

        public Smartphone(string name, float price, int id, float screenSize, OperationSystems os, float cameraPixels) : base(name, price, id)
        {
            ScreenSize = screenSize;
            OS = os;
            CameraPixels = cameraPixels;
        }

        public override void FillProcess()
        {
            Console.Write("Screen size: ");
            ScreenSize = Convert.ToSingle(Console.ReadLine());

            Console.Write("OS: ");
            OS = Enum.Parse<OperationSystems>(Console.ReadLine());

            Console.Write("Camera (pixels): ");
            CameraPixels = Convert.ToSingle(Console.ReadLine());

            base.FillProcess();
        }
    }
}