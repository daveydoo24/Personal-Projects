using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gum:InventoryItem
    {
        // public string VendMessage { get; } = "Chew Chew, Yum!";

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
