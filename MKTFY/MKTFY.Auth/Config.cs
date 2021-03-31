using IdentityModel;
using IdentityServer4;
//using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MKTFY.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] {JwtClaimTypes.Role } )
            };

        public static IEnumerable<ApiResource> ApiResources =>
          new List<ApiResource>
          {
                new ApiResource
                {
                    Name = "mktfyapi",
                    DisplayName = "MKTFY API",
                    Scopes = { "mktfyapi.scope", JwtClaimTypes.Role }
                }
          };

        public static IEnumerable<ApiScope> ApiScopes =>
           new List<ApiScope>
           {
                new ApiScope("mktfyapi.scope", "MKTFY API")
           };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("UzKjRFnAHffxUFati8HMjSEzwMGgGHmN".Sha256())
                    },
                    

                    // AllowedScopes = { "mktfy.scope", IdentityServerConstants.StandardScopes.OpenId }
                    AllowedScopes = { "mktfyapi.scope", "roles", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}

