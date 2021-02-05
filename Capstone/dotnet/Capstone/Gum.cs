using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gum:InventoryItem
    {
        public Gum(string slot, string name, decimal price) 
            : base(slot, name, price)
        {
        }

        public override string VendMessage()
        {
            return "Chew Chew, Yum!";
        }
    }
}
