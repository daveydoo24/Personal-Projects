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
            Console.Write("\nHow much do you want to deposit: ");
            string customerDeposit = Console.ReadLine();
            decimal customerDepositAmount = decimal.Parse(customerDeposit);
            CustomerBalance += customerDepositAmount;
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

            decimal customerChange = CustomerBalance;
            CustomerBalance -= customerChange;

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
