using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.DAO;
using TenmoServer.Models.TenmoServer.Security.Models;
using Microsoft.AspNetCore.Authorization;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private static IAccountsDAO accountDao;

        public AccountController(IAccountsDAO _accountDAO)
        {
            accountDao = _accountDAO;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Account> GetMyBalance(int id)
        {
            Account account = accountDao.GetBalance(id);
            if (account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
