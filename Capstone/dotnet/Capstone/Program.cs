using System;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendoMatic800 = new VendingMachine(); // start up vending machine

            Console.WriteLine("Welcome To The Vendo-Matic 800!");
            Console.WriteLine();
            bool customerExit = false;
            while (!customerExit)
            {
                DisplayMainMenu();
                string customerInput = Console.ReadLine();
                if (customerInput == "1")
                {
                    DisplayVendingMachineItems(vendoMatic800);
                }
                else if (customerInput == "2")
                {
                    vendoMatic800.PurchaseItem();
                }
                else if (customerInput == "3")
                {
                    Console.WriteLine("Thank you for using the Vendo-Matic 800!");
                    break;
                }
                else
                {
                    Console.WriteLine("Please try your selection again!");
                }

            }
            
            // Console.WriteLine($"vending machine bank balance: {vendoMatic800.bankAccount.Balance}");

        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine("");
            Console.Write("Please select an option: ");
        }

        static void DisplayVendingMachineItems(VendingMachine vendingMachine)
        {
            foreach (KeyValuePair<string, InventoryItem> item in vendingMachine.InventoryItems)
            {
                Console.WriteLine($"Slot: {item.Key} | { item.Value.Name} Price: {item.Value.Price} Qty: {item.Value.Quantity}");
            }

        }
    }
}
