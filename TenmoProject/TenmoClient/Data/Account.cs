using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Account
    {
        public int Account_id { get; set; }
        public int User_id { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"Your current balance is: {Balance:C2}";
        }
    }

}
