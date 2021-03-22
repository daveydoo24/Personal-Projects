using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Exceptions;

namespace TenmoClient
{
    public class AuthService
    {
        private API_User user = new API_User();
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private static IRestClient client = new RestClient(); // changed from readonly to static

        public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }
        
        //login endpoints
        public bool Register(LoginUser registerUser)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "login/register");
            request.AddJsonBody(registerUser);
            IRestResponse<API_User> response = client.Post<API_User>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return false;
            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    Console.WriteLine("An error message was received: " + response.Data.Message);
                }
                else
                {
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public API_User Login(LoginUser loginUser)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "login");
            request.AddJsonBody(loginUser);
            IRestResponse<API_User> response = client.Post<API_User>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return null;
            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    Console.WriteLine("An error message was received: " + response.Data.Message);
                }
                else
                {
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
                return null;
            }
            else
            {
                user.Token = response.Data.Token;
                client.Authenticator = new JwtAuthenticator(user.Token);
                return response.Data;
            }
        }

        public Account GetBalance()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "Account/"+ UserService.GetUserId());
           
            IRestResponse<Account> response = client.Get<Account>(request);
            
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public List<TransferRecord> GetTransfers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "transfer/transfers/" + UserService.GetUserId());

            IRestResponse <List<TransferRecord>> response = client.Get<List<TransferRecord>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public ReturnUser GetTheUserName(int id)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "transfer/usernames/"+id);

            IRestResponse<ReturnUser> response = client.Get<ReturnUser>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return response.Data;
        }

        public List<ReturnUser> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "transfer/users");

            IRestResponse<List<ReturnUser>> response = client.Get<List<ReturnUser>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public int TransferFunds(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "transfer");

            request.AddJsonBody(transfer);

            IRestResponse<int> response = client.Post<int>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return 0;
            ////need to figure out what is going on here
        }

        public void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new NoResponseException("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException("Please Login");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new ForbiddenException("no nono");
                }
                throw new NonSuccessException((int)response.StatusCode);
            }
        }
    }
}
