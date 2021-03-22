using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class TransferRecord
    {
        public int TransferId { get; set; }
        public string TransferType { get; set; }
        public string TransferStatus { get; set; }
        public int Account_from { get; set; }
        public int Account_to { get; set; }
        public decimal Amount { get; set; }


    }

    
}
