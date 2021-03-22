using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int Transfer_type_id { get; set; }
        public int Transfer_status_id { get; set; }
        public int Account_from { get; set; }
        public int Account_to { get; set; }
        public decimal Amount { get; set; }
    }

    
}
