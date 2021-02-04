using System;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            VendingMachine vendoMatic800 = new VendingMachine();

            foreach (KeyValuePair<string, InventoryItem> item in vendoMatic800.InventoryItems)
            {
                Console.WriteLine($"slot {item.Key} name { item.Value.Name} price {item.Value.Price} qty {item.Value.Quantity}" );
            }
        }
    }
}
