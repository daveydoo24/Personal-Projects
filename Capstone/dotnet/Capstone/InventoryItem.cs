using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    abstract class InventoryItem
    {
        // properties
        public string Slot { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; } = 5;


        public InventoryItem(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
        }

        // method to update quantity??
        // public void UpdateQuantity()

    }
}
