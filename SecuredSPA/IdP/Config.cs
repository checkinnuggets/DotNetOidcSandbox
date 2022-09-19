using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("greeting", "Greetings")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // JavaScript Client
                new Client
                {
                    ClientId = "secured-spa-example",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,

                    RedirectUris =           { "https://localhost:44303/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:44303/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:44303" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "greeting"
                    }
                }
            };
    }
}