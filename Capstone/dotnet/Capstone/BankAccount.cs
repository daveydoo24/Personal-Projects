using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class BankAccount
    {
        public decimal VendingMachineBalance { get; private set; } = 0.0M;
        public decimal CustomerBalance { get; private set; } = 0.0M;

        public void FeedMoney()
        {
            bool isCustomerFinished = false;
            while (!isCustomerFinished)
            {
                Console.WriteLine("\nPlease feed money now. Vendo-Matic 800 only accepts $1, $2, $5, or $10!");
                Console.Write("\nHow much do you want to deposit: ");
                string customerDeposit = Console.ReadLine();

                bool validInput = Int32.TryParse(customerDeposit, out int customerDepositAmount);
                // decimal customerDepositAmount = decimal.Parse(customerDeposit); 
                if (validInput)
                {
                    if (customerDepositAmount == 1.0M || customerDepositAmount == 2.0M || customerDepositAmount == 5.0M || customerDepositAmount == 10.0M)
                    {
                        CustomerBalance += customerDepositAmount;

                        string dateTimeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
                        string logMessage = $"{dateTimeStamp} FEED MONEY: {customerDepositAmount:C2} {CustomerBalance:C2}";
                        
                        Audit.Log(logMessage);
                        
                        Console.WriteLine($"\nYour current balance is: {CustomerBalance:C2}\n");
                        
                        bool continueFeedMoney = true;
                        while (continueFeedMoney)
                        {
                            Console.Write("Would you like to feed more money into the Vendo-Matic 800? Please enter (y) or (n): ");
                            string customerResponse = Console.ReadLine();
                            if (customerResponse.ToLower() == "y")
                            {
                                break;
                            }
                            else if (customerResponse.ToLower() == "n")
                            {
                                isCustomerFinished = true;
                                continueFeedMoney = false;
                            }
                            else
                            {
                                Console.WriteLine("Please re-enter your choice!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid amount!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid amount!");
                }
            }
        }

        public void MakePurchase(decimal purchaseAmount)
        {
            CustomerBalance -= purchaseAmount;
            VendingMachineBalance += purchaseAmount;
        }

        public void FinishTransaction()
        {
            int numOfQuarters = 0;
            int numOfDimes = 0;
            int numOfNickels = 0;
            string dateTimeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
            string customerBalanceBeforePurchase = $"{CustomerBalance:C2}";

            decimal customerChange = CustomerBalance;
            CustomerBalance -= customerChange;
            string customerBalanceAfterPurchase = $"{CustomerBalance:C2}";
            string logMessage = $"{dateTimeStamp} GIVE CHANGE: {customerBalanceBeforePurchase} {customerBalanceAfterPurchase}";

            Audit.Log(logMessage);


            int changeInCoins = (int)(customerChange * 100);
            
            numOfQuarters = changeInCoins / 25;
            changeInCoins -= (numOfQuarters * 25);

            numOfDimes = changeInCoins / 10;
            changeInCoins -= (numOfDimes * 10);

            numOfNickels = changeInCoins / 5;
            changeInCoins -= (numOfNickels * 5);

            Console.WriteLine($"\nYour change is {numOfQuarters} Quarters, {numOfDimes} Dimes, {numOfNickels} Nickels\n");
        }

    }
}
