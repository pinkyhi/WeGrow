using IdentityServer4.Models;

namespace WeGrow.Auth
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("WeGrow.read"), new ApiScope("WeGrow.write"), };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("WeGrow")
                {
                    Scopes = new List<string> { "WeGrow.read", "WeGrow.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "WeGrow.read", "WeGrow.write" }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5445/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5445/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5445/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "WeGrow.read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
            };
    }
}
