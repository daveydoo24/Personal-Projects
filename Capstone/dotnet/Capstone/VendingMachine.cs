using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    class VendingMachine
    {
        // properties
        // inventory - dictionary - key=slot & value=inventory item
        public Dictionary<string, InventoryItem> InventoryItems = new Dictionary<string, InventoryItem>();

        // constructor
        public VendingMachine()
        {
            LoadInventory();
        }

        // methods
        // load up initial inventory
        
        public void LoadInventory()
        {
            string directory = Environment.CurrentDirectory;
            string filename = "vendingmachine.csv";
            string fullPath = Path.Combine(directory, filename);

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] words = line.Split('|');
                        // slot[0] name[1] price[2] type[3]
                        string slot = words[0];
                        string name = words[1];
                        decimal price = decimal.Parse(words[2]);
                        string type = words[3].ToLower();

                        if (type == "chip")
                        {
                            Chip chip = new Chip(slot, name, price);
                            InventoryItems.Add(slot, chip);
                        }
                        else if (type == "gum")
                        {
                            Gum gum = new Gum(slot, name, price);
                            InventoryItems.Add(slot, gum);

                        }
                        else if (type == "candy")
                        {
                            Candy candy = new Candy(slot, name, price);
                            InventoryItems.Add(slot, candy);
                        }
                        else if (type == "drink")
                        {
                            Drink drink = new Drink(slot, name, price);
                            InventoryItems.Add(slot, drink);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }
            
        }

    }
}
