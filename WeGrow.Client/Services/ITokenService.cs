using IdentityModel.Client;

namespace WeGrow.Client.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
