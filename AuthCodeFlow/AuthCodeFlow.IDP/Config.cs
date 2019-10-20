using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace AuthCodeFlow.IDP
{
    public static class Config
    {
        public static List<TestUser> Users = new List<TestUser>
                {
                    new TestUser
                    {
                        Username = "houssem",
                        Password = "secret"
                    },
                    new TestUser
                    {
                        Username = "samir",
                        Password = "samir"
                    }
                };
        public static List<Client> Clients = new List<Client>
                {

                    new Client
                    {
                        ClientId = "mvc",
                        ClientName = "ASP;NET CORE MVC APP",
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedGrantTypes = GrantTypes.Code,
                        AllowedScopes = { "api" }
                    }
        };
        public static List<ApiResource> Apis = new List<ApiResource>
        {
               new ApiResource("api", "API")
        };
    }
}
