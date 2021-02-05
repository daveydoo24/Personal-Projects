using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class BankAccount
    {
        public decimal VendingMachineBalance { get; private set; } = 0.0M;
        public decimal CustomerBalance { get; private set; } = 0.0M;

        public void AddFunds(decimal deposit)
        {
            VendingMachineBalance += deposit;
        }

        public void UpdateBalance(decimal refund)
        {
            VendingMachineBalance -= refund;
        }

        public void FeedMoney()
        {
            Console.Write("How much do you want to deposit: ");
            string customerDeposit = Console.ReadLine();
            decimal customerDepositAmount = decimal.Parse(customerDeposit);
            CustomerBalance += customerDepositAmount;
        }

        public void MakePurchase(decimal purchaseAmount)
        {
            CustomerBalance -= purchaseAmount;
            VendingMachineBalance += purchaseAmount;
        }

    }
}
