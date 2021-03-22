using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public int Create(Transfer transfer)
        {
            int newId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    const int transferType = 2; // represents transfer type of "send" in SQL transfer table
                    const int transferStatus = 2; // represents transfer status of "approved" in SQL transfer table
                    conn.Open();
                    string sql = "BEGIN TRANSACTION;" +
                                    "INSERT INTO transfers (transfer_type_id,transfer_status_id,account_from,account_to,amount)" +
                                    "VALUES(@trans_type, @trans_status, (SELECT accounts.account_id FROM accounts WHERE user_id = @account_from),(SELECT accounts.account_id FROM accounts WHERE user_id = @account_to) , @amount);" +
                                    "SELECT SCOPE_IDENTITY();" +
                                    "COMMIT TRANSACTION;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@trans_type", transferType);
                    cmd.Parameters.AddWithValue("trans_status", transferStatus);
                    cmd.Parameters.AddWithValue("@account_from", transfer.Account_from);
                    cmd.Parameters.AddWithValue("@account_to", transfer.Account_to);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newId;
        }
        
        // update 2 users, one to add to, one to subtract from. 2 expected output of returned rows
        public int UpdateBalances(Transfer transfer)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlUpdateAdd = "UPDATE accounts SET accounts.balance = accounts.balance + @amount WHERE accounts.account_id = (SELECT accounts.account_id FROM accounts WHERE user_id = @account_to);";
                    string sqlUpdateSubtract = "UPDATE accounts SET accounts.balance = accounts.balance-@amount WHERE accounts.account_id = (SELECT accounts.account_id FROM accounts WHERE user_id = @account_from);";

                    SqlCommand cmd = new SqlCommand(sqlUpdateAdd, conn);

                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@account_to", transfer.Account_to);

                    rowsUpdated = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(sqlUpdateSubtract, conn);

                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@account_from", transfer.Account_from);

                    rowsUpdated += cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rowsUpdated;
        }

        public IList<TransferRecord> GetTransferList(int id)
        {
            List<TransferRecord> transfers = new List<TransferRecord>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlString = "SELECT t.transfer_id, tt.transfer_type_desc, ts.transfer_status_desc, t.account_from, t.account_to, t.amount FROM transfers t JOIN transfer_types tt ON tt.transfer_type_id = t.transfer_type_id JOIN transfer_statuses ts ON ts.transfer_status_id = t.transfer_status_id WHERE t.account_to = (SELECT account_id FROM accounts WHERE user_id = @userId) OR t.account_from = (SELECT account_id FROM accounts WHERE user_id = @userId); ";

                    SqlCommand cmd = new SqlCommand(sqlString, conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TransferRecord transfer = new TransferRecord();

                        transfer.Account_from = Convert.ToInt32(reader["account_from"]);
                        transfer.Account_to = Convert.ToInt32(reader["account_to"]);
                        transfer.Amount = Convert.ToDecimal(reader["amount"]);
                        transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
                        transfer.TransferStatus = Convert.ToString(reader["transfer_status_desc"]);
                        transfer.TransferType = Convert.ToString(reader["transfer_type_desc"]);

                        transfers.Add(transfer);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return transfers;
        }

        public ReturnUser GetUserName(int id)
        {
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT users.username, users.user_id FROM users WHERE users.user_id =(SELECT user_id FROM accounts WHERE account_id=@userId)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        returnUser.Username = Convert.ToString(reader["username"]);
                        returnUser.UserId = Convert.ToInt32(reader["user_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnUser;
        }
    }
}
