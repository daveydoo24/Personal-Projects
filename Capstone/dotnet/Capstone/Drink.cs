using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Drink: InventoryItem
    {
        public override string VendMessage { get; } = "Glug Glug, Yum!";
        public Drink (string slot, string name, decimal price)
            : base(slot, name, price)
        {
        }
    }
}
