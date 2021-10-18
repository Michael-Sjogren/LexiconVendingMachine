using System;
using VendingMachineLib;

namespace VendingMachineConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var vendor = new VendingMachine(CurrencyType.SEK, new uint[] {1, 5, 20, 50, 100, 500, 1000});
            Console.WriteLine("Hello World!");
        }
    }
}