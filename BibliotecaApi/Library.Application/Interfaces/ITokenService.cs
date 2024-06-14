using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BibliotecaApi.Library.Application.Interfaces
{ 
    public interface ITokenService
    {
        JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExperiredToken(string token, IConfiguration _config);
    }
}
 