using System;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendoMatic800 = new VendingMachine(); // start up vending machine
            Console.WriteLine("Welcome To The Vendo-Matic 800!\n");
            
            bool customerExit = false;
            while (!customerExit)
            {
                VendingMachineDisplay.DisplayMainMenu();

                Console.Write("Please select an option: ");
                string customerInput = Console.ReadLine();

                if (customerInput == "1")
                {
                    VendingMachineDisplay.DisplayVendingMachineItems(vendoMatic800.InventoryItems);
                }
                else if (customerInput == "2")
                {
                    vendoMatic800.PurchaseItem();
                }
                else if (customerInput == "3")
                {
                    Console.WriteLine("\nThank you for using the Vendo-Matic 800!");
                    customerExit = true;
                }
                else
                {
                    Console.WriteLine("Please try your selection again!");
                }
            }
        }
    }
}
