using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Chip: InventoryItem
    {
        public override string VendMessage { get; } = "Crunch Crunch, Yum!";
        public Chip (string slot, string name, decimal price)
            : base(slot, name, price)
        {
        }
    }
}
