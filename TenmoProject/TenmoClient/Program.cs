using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            int loginRegister = -1;
            while (loginRegister != 1 && loginRegister != 2)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out loginRegister))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (loginRegister == 1)
                {
                    while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                    {
                        LoginUser loginUser = consoleService.PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                        }
                    }
                }
                else if (loginRegister == 2)
                {
                    bool isRegistered = false;
                    while (!isRegistered) //will keep looping until user is registered
                    {
                        LoginUser registerUser = consoleService.PromptForLogin();
                        isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                            loginRegister = -1; //reset outer loop to allow choice for login
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }

            MenuSelection();
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (menuSelection == 1)
                {
                    try
                    {
                        Account account = authService.GetBalance();
                        if (account != null)
                        {
                            consoleService.DisplayCurrentBalance(account.ToString());
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }                
                }
                else if (menuSelection == 2)
                {
                    List<TransferRecord> transferList = authService.GetTransfers();
                    consoleService.DisplayAllTransfers(transferList);
                    while (menuSelection != -1)
                    {
                        int transferId = consoleService.GetInteger("Enter Transfer Id to get more info (0 to cancel)");
                        TransferRecord transferRecordToDisplay = FindValidTransferRecord(transferList, transferId);

                        if (transferId == 0)
                        {
                            menuSelection = -1;
                        }
                        else if (transferRecordToDisplay != null)
                        {
                            ReturnUser userAccountFrom = authService.GetTheUserName(transferRecordToDisplay.Account_from);
                            ReturnUser userAccountTo = authService.GetTheUserName(transferRecordToDisplay.Account_to);

                            Console.WriteLine("------------------------");
                            Console.WriteLine("Transfer Details");
                            Console.WriteLine("------------------------");
                            Console.WriteLine($"Id: {transferRecordToDisplay.TransferId}");
                            Console.WriteLine($"From: {userAccountFrom.Username}");
                            Console.WriteLine($"To: {userAccountTo.Username}");
                            Console.WriteLine($"Status: {transferRecordToDisplay.TransferStatus}");
                            Console.WriteLine($"Amount: {transferRecordToDisplay.Amount:C2}");
                            Console.WriteLine($"Type: {transferRecordToDisplay.TransferType}");

                            menuSelection = -1;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Transfer ID requested");
                        }
                    }
                }
                else if (menuSelection == 3)
                {
                    
                }
                else if (menuSelection == 4)
                {
                    Transfer transfer = new Transfer();
                    consoleService.DisplayAvailableUsers();
                    int userId = consoleService.GetInteger("Enter ID of user you are sending to (enter '0' to cancel): ");
                    List<int> listOfId = ListOfUserId();
                    
                    if (userId != 0 && listOfId.Contains(userId))
                    {
                        int newTransferID;
                        bool amountIsValid = false;
                        decimal amountToTransfer = 0.0M;
                        while (!amountIsValid)
                        {
                            amountToTransfer = consoleService.GetDecimal("Enter amount to transfer: ");
                            Account account = authService.GetBalance();
                            decimal availableBalance = account.Balance;

                            if (availableBalance >= amountToTransfer && amountToTransfer > 0)
                            {
                                amountIsValid = true;
                                transfer.Account_from = UserService.GetUserId();
                                transfer.Account_to = userId;
                                transfer.Amount = amountToTransfer;

                                newTransferID = authService.TransferFunds(transfer);
                                Console.WriteLine($"Transfer successful. New transfer ID is: {newTransferID}");
                            }
                            else
                            {
                                if(amountToTransfer <= 0)
                                {
                                    Console.WriteLine("Transfer amount must be greater than 0.");
                                }
                                else
                                {
                                    Console.WriteLine("Transfer amount requested exceeds available balance. Please re-enter amount to transfer.");
                                    consoleService.DisplayCurrentBalance(account.ToString());
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        if(userId == 0)
                        {
                            menuSelection = -1;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Please input a valid user Id"); //want to return to transfer screen
                            menuSelection = 4;
                            continue;
                        }
                    }
                }
                else if (menuSelection == 5)
                {

                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    Run(); //return to entry point
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }

        private static TransferRecord FindValidTransferRecord (List<TransferRecord> transferRecords, int transferRecordId)
        {
            TransferRecord newRecord = null;
            foreach (TransferRecord record in transferRecords)
            {
                if(record.TransferId == transferRecordId)
                {
                    newRecord = record;
                }
            }
            return newRecord;
        }

        private static List<int> ListOfUserId()
        {
            List<int> userIdList = new List<int>();
            foreach (ReturnUser myUser in authService.GetUsers())
            {
                userIdList.Add(myUser.UserId);
            }
            return userIdList;
        }
    }
}
