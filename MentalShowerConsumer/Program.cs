using System;

namespace MentalShowerConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Worker worker = new Worker();
            Console.WriteLine($"Readings Coming in:");
            worker.Start();

            Console.ReadLine();



        }
    }
}
