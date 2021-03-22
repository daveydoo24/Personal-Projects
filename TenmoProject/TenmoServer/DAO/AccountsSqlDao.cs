using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.Models.TenmoServer.Security.Models;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class AccountsSqlDao : IAccountsDAO
    {
        private readonly string connectionString;

        public AccountsSqlDao(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public Account GetBalance(int id)
        {
            
            Account account = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT accounts.account_id,accounts.balance,accounts.user_id FROM accounts JOIN users ON users.user_id = accounts.user_id WHERE accounts.user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        account.User_id = Convert.ToInt32(reader["user_id"]);
                        account.Account_id = Convert.ToInt32(reader["account_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return account;
        }
    }
}
