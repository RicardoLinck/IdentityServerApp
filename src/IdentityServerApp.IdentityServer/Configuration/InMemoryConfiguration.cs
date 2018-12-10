using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;

namespace IdentityServerApp.IdentityServer.Configuration
{
    public sealed class InMemoryConfiguration
    {
        public IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client{
                    ClientId = "ApiClient",
                    ClientName = "ApiClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new []{new Secret("secret".Sha256())},
                    AllowedScopes = new []{"IdentityServerApp.Api"},
                }
            };
        }

        public IEnumerable<ApiResource> GetResources()
        {
            return new ApiResource[]
            {
                new ApiResource("IdentityServerApp.Api"," Identity Server App - API")
            };
        }
    }
}