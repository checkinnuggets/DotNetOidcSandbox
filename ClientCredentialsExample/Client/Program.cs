using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {

        // Url of the identity provider - in this case, IdentityServer
        private const string Idp = "https://localhost:44399";
        
        // Client ID and Secret are owned by the IDP
        private const string ClientId = "test-api-client";
        private const string ClientSecret = "this15secure";

        private const string ApiEndpoint = "https://localhost:44301/greetings";
        private const string ApiScope = "greeting";


        private static async Task Main()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Idp);
            if (disco.IsError)
            {
                Console.WriteLine("Error fetching discovery document:");
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,

                Scope = ApiScope
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine("Token response error:");
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine("Token response:");
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync(ApiEndpoint);

            Console.WriteLine("API response:");
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