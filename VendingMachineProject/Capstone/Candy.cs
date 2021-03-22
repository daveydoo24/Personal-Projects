using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Candy: InventoryItem
    {
        public override string VendMessage { get; } = "Munch Munch, Yum!";
        public Candy (string slot, string name, decimal price)
            : base(slot, name, price)
        {
        }
    }
}
