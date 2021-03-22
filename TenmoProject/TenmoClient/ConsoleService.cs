using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AuthService authService = new AuthService();

        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public void DisplayAvailableUsers()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID          Name");
            Console.WriteLine("------------------------------------");

            foreach (ReturnUser myUser in authService.GetUsers())
            {
                if (myUser.UserId != UserService.GetUserId())
                {
                    Console.WriteLine($"{myUser.UserId}        {myUser.Username}");
                }
            }
        }
        public void DisplayAllTransfers(List<TransferRecord> transfers)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID    Account From     Account To       Amount");
            Console.WriteLine("------------------------------------");
            
            foreach (TransferRecord transfer in transfers)
            {
                int accontNumberFrom = transfer.Account_from;
                int accountNumberTo = transfer.Account_to;
                ReturnUser userAccountFrom= authService.GetTheUserName(accontNumberFrom);
                ReturnUser userAccountTo = authService.GetTheUserName(accountNumberTo);
                
                Console.WriteLine($"{transfer.TransferId}              {userAccountFrom.Username}            {userAccountTo.Username}         {transfer.Amount:C2}");
            }
        }

        public void DisplayCurrentBalance(string availableBalance)
        {
            Console.WriteLine(availableBalance);
        }

        public int GetInteger(string message)
        {
            string userInput = string.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));
            
            return intValue;
        }

        public bool IsIntPositive(int valueToBeTested)
        {

            if (valueToBeTested > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetDecimal(string message)
        {
            string userInput = string.Empty;
            decimal decimalValue = 0.0M;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!decimal.TryParse(userInput, out decimalValue));

            return decimalValue;
        }


    }
}
