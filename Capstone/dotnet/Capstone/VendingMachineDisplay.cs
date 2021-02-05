using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    abstract class VendingMachineDisplay
    {
        public static void DisplayMainMenu()
        {
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine("");
        }
        public static void DisplayVendingMachineItems(Dictionary<string, InventoryItem> inventoryItems)
        {
            Console.WriteLine();
            foreach (KeyValuePair<string, InventoryItem> item in inventoryItems)
            {
                Console.WriteLine($"Slot: {item.Key} | { item.Value.Name} Price: {item.Value.Price} Qty: {item.Value.AvailableQuantity}");
            }
            Console.WriteLine();
        }

        public static void DisplayPurchaseMenu(BankAccount bankAccount)
        {
            Console.WriteLine("\nPurchase Menu");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction\n");
            Console.WriteLine($"Current Money Provided: {bankAccount.CustomerBalance:C2}\n");
        }
    }
}
