
using BusinessObjects.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessObjects.Interfaces
{
    public interface IAuthService
    {
      Task<AuthModel> RegisterAsync(RegisterMoldel model);
        Task<AuthModel> GetTokenAsync(TokenRquestMoldel model);
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
