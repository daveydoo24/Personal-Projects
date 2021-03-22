using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;


namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        int Create(Transfer transfer);

        int UpdateBalances(Transfer transfer);

        public IList<TransferRecord> GetTransferList(int id);
      
        public ReturnUser GetUserName(int id);

    }
}
