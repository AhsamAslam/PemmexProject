using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks; 

namespace Authentication.API
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "pemmexclient",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientName = "Pemmex Client Web App",
                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                // scopes that client has access to
                RedirectUris = { "http://localhost:3000/callback" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "organizationapi"
                }
            }
            //new Client
            //{
            //    ClientId = "pemmex_mvc_client",
            //    ClientName = "Pemmex Client Web App",
            //    ClientSecrets = { new Secret("secret".Sha256()) },
            //    AllowedGrantTypes = GrantTypes.Code,
            //    AllowAccessTokensViaBrowser = true,


            //    // where to redirect to after login
            //    RedirectUris = { "http://localhost:3000" },

            //    // where to redirect to after logout
            //    PostLogoutRedirectUris = { "http://localhost:3000" },
            //    AllowedCorsOrigins = { "http://localhost:3000" },
            //    AllowedScopes = new List<string>
            //    {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        IdentityServerConstants.StandardScopes.Profile,
            //        "organizationapi"
            //    }
            //}
        };
        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("organizationapi", "Organization Api")
        };
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api", "Pemmex Oy")
            };
        }
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1234",
                    Username = "ahsam",
                    Password = "ahsam",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName,"ahsam"),
                        new Claim(JwtClaimTypes.FamilyName,"aslam")
                    }
                }
            };

    }
}
