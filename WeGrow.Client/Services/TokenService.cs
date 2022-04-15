using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace WeGrow.Client.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> identityServerSettings;
        private readonly IOptions<AdminIdentityServerSettings> adminIdentityServerSettings;
        private readonly IHttpContextAccessor accessor;
        public readonly DiscoveryDocumentResponse discoveryDocument;
        private readonly HttpClient httpClient;

        public TokenService(IOptions<IdentityServerSettings> identityServerSettings, IOptions<AdminIdentityServerSettings> adminIdentityServerSettings, IHttpContextAccessor accessor)
        {
            this.identityServerSettings = identityServerSettings;
            this.adminIdentityServerSettings = adminIdentityServerSettings;
            this.accessor = accessor;
            httpClient = new HttpClient();
            discoveryDocument = httpClient.GetDiscoveryDocumentAsync
                (this.identityServerSettings.Value.DiscoveryUrl).Result;
            if (discoveryDocument.IsError)
            {
                throw new Exception($"Unable to get discovery document\n{discoveryDocument.Error}",
                    discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetAdminToken(string scope)
        {
            if(accessor.HttpContext?.User?.IsInRole("admin") == true)
            {
                var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new
                ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = adminIdentityServerSettings.Value.ClientName,
                    ClientSecret = adminIdentityServerSettings.Value.ClientPassword,
                    Scope = scope
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Unable to get token", tokenResponse.Exception);
                }
                return tokenResponse;
            }
            else
            {
                throw new Exception("You are not admin");
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new
                ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = identityServerSettings.Value.ClientName,
                ClientSecret = identityServerSettings.Value.ClientPassword,
                Scope = scope
            });
            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }
            return tokenResponse;
        }
    }
}
