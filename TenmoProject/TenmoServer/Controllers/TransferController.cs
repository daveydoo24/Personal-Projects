using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Models.TenmoServer.Security.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private static IUserDAO userDAO;
        private static ITransferDAO transferDAO;

        public TransferController(IUserDAO _userDAO, ITransferDAO _transferDAO)
        {
            userDAO = _userDAO;
            transferDAO = _transferDAO;
        }

        [HttpGet("users")]
        public ActionResult<List<ReturnUser>> GetUserList()
        {
            List<ReturnUser> returnUsers = userDAO.GetReturnUsers();
            if (returnUsers != null)
            {
                return Ok(returnUsers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<int> CreateTransfer(Transfer transfer)
        {
            int newTransferId = transferDAO.Create(transfer);
            if (newTransferId > 0)
            {
                int completed = transferDAO.UpdateBalances(transfer);
                if (completed == 2)
                {
                    return Ok(newTransferId);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("transfers/{id}")]
        public ActionResult<IList<TransferRecord>> GetTransfersList(int id)
        {
            IList<TransferRecord> transfers = transferDAO.GetTransferList(id);
            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("usernames/{id}")]
        public ActionResult<ReturnUser> Username(int id)
        {
            ReturnUser returnUser = transferDAO.GetUserName(id);
            if (returnUser != null)
            {
                return Ok(returnUser);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
