using System;
using System.IO;
using System.Linq;
using VendingMachineLib;

namespace VendingMachineConsoleApp
{
        public enum Command
        {
            unknown,
            ins,
            buy,
            quit
        }
        public class App
        {
            private VendingMachine _vendor;
            public void Run()
            {
                
                var isRunning = true;
                _vendor = new VendingMachine(
                    CurrencyType.SEK,
                    new int[] {1, 5, 10, 20, 50, 100, 500, 1000}
                );
                _vendor.FillVendorWithProducts();
                var info =
                    $"Enter: '{Command.ins.ToString()}' to insert money " +
                    $"or '{Command.buy.ToString()}' to purchase a product." +
                    $"\nAnd '{Command.quit.ToString()}' to quit the application.";
                
                while (isRunning)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to this vending Machine!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(info);
                    Console.ResetColor();
                    Console.WriteLine("Here is a list of all products:");
                    Console.WriteLine(_vendor.ShowAllProducts());
                    Console.WriteLine();
                    Console.WriteLine($"Money Inserted: {_vendor.TotalMoneyInserted}" );
                    var cmd = GetCommandFromUserInput();
                    switch (cmd)
                    {
                        case Command.quit:
                            isRunning = false;
                            break;
                        case Command.buy:
                            Buy();
                            break;
                        case Command.unknown:
                            Console.WriteLine("Unknown Command.");
                            Console.ReadKey();
                            break;
                        case Command.ins:
                            Insert();
                            break;
                        default:
                            Console.WriteLine("Unknown Command.");
                            Console.ReadKey();
                            break;
                    }
                }           
            }

            private void Insert()
            {
                Console.WriteLine("Type in a number to insert money into the machine.");
                Console.WriteLine("Here is a list of all valid values you can insert:\n");
                foreach (var value in _vendor.Denominations)
                {
                    Console.Write(value);
                    Console.Write(", ");
                }
                Console.WriteLine();
                var money = GetNumberFromUserInput();
                try
                {
                    _vendor.InsertMoney(money);
                }
                catch (Exception e)
                {
                    PrintError(e.Message);
                }
            }
            private void Buy()
            {
                try
                {
                    var option = GetNumberFromUserInput();
                    var product = _vendor.Purchase(option);
                    var change = _vendor.EndTransaction();
                    Console.WriteLine("Purchase Successful!");
                    Console.WriteLine($"Your purchased product: {product.ProductName}");
                    Console.WriteLine($"Your change: {change.Sum()} {_vendor.Currency}");
                    Console.WriteLine($"Use: {product.Use()}");
                    Console.WriteLine($"Examine: {product.Examine()}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    PrintError(e.Message);
                }
            }
            
            private Command GetCommandFromUserInput()
            {
                Console.Write("Command: ");
                var input = GetInput();
                if (string.IsNullOrEmpty(input)) return Command.unknown;
                input = input.Trim().ToLower();
  
                if(input.Equals(Command.buy.ToString().ToLower()))
                {
                    return Command.buy;
                }
                if(input.Equals(Command.ins.ToString()))
                {
                    return Command.ins;
                }
                
                if(input.Equals(Command.quit.ToString()))
                {
                    return Command.quit;
                }

                return Command.unknown;
            }
            
            private int GetNumberFromUserInput()
            {
                Console.Write("Enter a number: ");
                var input = GetInput();
                int num;
                if (int.TryParse(input, out num))
                {
                    return num;
                }
                return -1;
            }

            private string GetInput()
            {
                try
                {
                    var input = Console.ReadLine();
                    return input;

                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (OutOfMemoryException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }

                return "";
            }

            private void PrintError(string msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg);
                Console.ResetColor();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
}