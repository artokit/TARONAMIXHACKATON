using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Common.Users;

namespace Application.Services.Common;

public static class Jwt
{
    public static int GetUserId(this string jwt)
    {
        var claims = jwt.GetClaims();
        
        return Convert.ToInt32(claims.First(t => t.Type.ToString() == ClaimTypes.UserId.ToString()).Value);
    }

    public static IEnumerable<Claim> GetClaims(this string jwt)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(jwt.Split(" ")[1]).Claims;
    }
}