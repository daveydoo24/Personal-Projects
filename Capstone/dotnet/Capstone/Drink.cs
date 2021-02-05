using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Drink: InventoryItem
    {
        public Drink (string slot, string name, decimal price)
            : base(slot, name, price)
        {
        }

        public override string VendMessage()
        {
            return "Glug Glug, Yum!";
        }
    }
}
