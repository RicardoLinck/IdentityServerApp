using System;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;

namespace IdentityServerApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(10000);
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            // request token
            var tokenClient = new HttpClient();
            var tokenResponse = await tokenClient.RequestTokenAsync(new TokenRequest
            {
                Address = "http://identityserver/connect/token",
                GrantType = "client_credentials",

                ClientId = "ApiClient",
                ClientSecret = "secret",
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://identityserver-api/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }
}
