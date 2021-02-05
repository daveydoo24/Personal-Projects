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

        public BankAccount bankAccount = new BankAccount();

        // constructor
        public VendingMachine()
        {
            LoadInventory();
        }

        // methods
        // purchase item method
        public void PurchaseItem()
        {
            bool purchaseComplete = false;
            while (!purchaseComplete)
            {
                Console.WriteLine("\nPurchase Menu");
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction\n");
                Console.WriteLine($"Current Money Provided: {bankAccount.CustomerBalance:C2}\n");
                Console.Write("Please make a selection: ");
                string customerInput = Console.ReadLine();

                if (customerInput == "1")
                {
                    bankAccount.FeedMoney();
                }
                else if (customerInput == "2")
                {
                    SelectProduct(InventoryItems);
                }
                else if (customerInput == "3")
                {
                    bankAccount.FinishTransaction();
                    purchaseComplete = true;
                }
                else
                {
                    Console.WriteLine("Please try your selection again!");
                }
            }
        }

        public void SelectProduct(Dictionary<string, InventoryItem> inventoryItems)
        {
            foreach (KeyValuePair<string, InventoryItem> item in inventoryItems)
            {
                Console.WriteLine($"Slot: {item.Key} | { item.Value.Name} Price: {item.Value.Price} Qty: {item.Value.Quantity}");
            }
            Console.WriteLine();
            Console.Write("Please enter product ID: ");
            string userInput = Console.ReadLine();

            if (!inventoryItems.ContainsKey(userInput))
            {
                Console.WriteLine("Product ID does not exist");
            }
            else if (inventoryItems[userInput].Quantity == 0)
            {
                Console.WriteLine("Product is SOLD OUT");
            }
            else
            {
                if(bankAccount.CustomerBalance > inventoryItems[userInput].Price)
                {
                    inventoryItems[userInput].Quantity--;
                    Console.WriteLine($"Selected {inventoryItems[userInput].Name} for ${inventoryItems[userInput].Price}");
                    Console.WriteLine($"{inventoryItems[userInput].VendMessage()}");
                    bankAccount.MakePurchase(inventoryItems[userInput].Price);
                    Console.WriteLine($"Your available balance is now: ${bankAccount.CustomerBalance}");

                }
                else
                {
                    Console.WriteLine("Insufficient funds - please add money");
                }
            }
        }

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
                Console.WriteLine("There was an error reading the input file");
            }   
        }
    }
}
