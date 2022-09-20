using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Pemmex.Identity.Data
{
    public static class Config
    {
        public static string origin = Startup.StaticConfig.GetSection("AllowedChatOrigins").Value;
        public static List<Client> Clients = new List<Client>
        {
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "Organization.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "Compensation.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "EmailServices", "write", "read", "openid", "profile", "email","roles"},
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true

                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowedScopes = { "Holidays.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "Notifications.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "TaskManager.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "Pemmex.Identity", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "PemmexAPIAggregator.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "EmployeeTargets.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = "pemmex-identity-api",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { origin + "/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { origin },
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                    AllowedScopes = { "Survey.API", "write", "read", "openid", "profile", "email","roles" },
                    AllowedCorsOrigins = new List<string>
                    {
                        origin
                    },
                    AccessTokenLifetime = 86400,
                    UpdateAccessTokenClaimsOnRefresh = true
                }

        };
        public static List<IdentityResource> IdentityResources = new List<IdentityResource>
        {
             new IdentityResources.OpenId(),
             new IdentityResources.Profile(),
             new IdentityResource("roles", "User role(s)", new List<string> { "role" })
        };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "Organization.API",
                DisplayName = "Organization API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "Compensation.API",
                DisplayName = "Compensation API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "EmailServices",
                DisplayName = "Email Services",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "Holidays.API",
                DisplayName = "Holidays API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "Notification.API",
                DisplayName = "Notification API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "TaskManager.API",
                DisplayName = "TaskManager API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "PemmexAPIAggregator.API",
                DisplayName = "PemmexAPIAggregator API",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "Pemmex.Identity",
                DisplayName = "Pemmex Identity",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "EmployeeTargets.API",
                DisplayName = "Employee Targets",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            },
            new ApiResource
            {
                Name = "Survey.API",
                DisplayName = "Survey",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Role
                },
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            }
            
        };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {

            new ApiScope("email"),
            new ApiScope("read"),
            new ApiScope("write"),
            new ApiScope("Organization.API"),
            new ApiScope("TaskManager.API"),
            new ApiScope("Notification.API"),
            new ApiScope("Holidays.API"),
            new ApiScope("Compensation.API"),
            new ApiScope("EmailServices.API"),
            new ApiScope("Pemmex.Identity"),
            new ApiScope("PemmexAPIAggregator.API"),
            new ApiScope("EmployeeTargets.API"),
            new ApiScope("Survey.API")
        };
    }
}
