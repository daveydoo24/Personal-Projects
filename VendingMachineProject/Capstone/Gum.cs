using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gum:InventoryItem
    {
        public override string VendMessage { get; } = "Chew Chew, Yum!";
        public Gum(string slot, string name, decimal price) 
            : base(slot, name, price)
        {
        }
    }
}
