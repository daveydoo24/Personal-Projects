using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    abstract class InventoryItem
    {
        public string Slot { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; set; } = 5;
        public virtual string VendMessage { get; }

        public string AvailableQuantity
        {
            get
            {
                if (Quantity == 0)
                {
                    return "SOLD OUT";
                }
                else
                {
                    return Quantity.ToString();
                }
            }
        }

        public InventoryItem(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
        }
    }
}
