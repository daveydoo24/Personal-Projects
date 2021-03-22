using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
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
