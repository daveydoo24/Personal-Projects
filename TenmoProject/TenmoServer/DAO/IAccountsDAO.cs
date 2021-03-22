﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.Models.TenmoServer.Security.Models;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public interface IAccountsDAO
    {
        Account GetBalance(int id);
    }
}
