using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class TransferRecord
    {
        public int TransferId { get; set; }
        public string TransferType { get; set; } // type as string value
        public string TransferStatus { get; set; } // status as string value
        public int Account_from { get; set; }
        public int Account_to { get; set; }
        public decimal Amount { get; set; }


    }
}
