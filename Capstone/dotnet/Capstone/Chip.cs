using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Chip: InventoryItem
    {
        public Chip (string slot, string name, decimal price)
            : base(slot, name, price)
        {

        }

    }
}
