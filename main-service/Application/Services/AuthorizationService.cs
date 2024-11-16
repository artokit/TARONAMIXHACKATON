using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Exceptions.Authorization;
using Application.Mappers;
using Application.Services.Common;
using Application.Services.Common.Settings;
using Contracts.Authorization.Requests;
using Contracts.Authorization.Responses;
using Domain.Common.Users;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = Application.Services.Common.ClaimTypes;

namespace Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private IUserRepository _userRepository;
    private AuthOptions _authOptions;
    
    public AuthorizationService(IUserRepository userRepository, AuthOptions authOptions)
    {
        _userRepository = userRepository;
        _authOptions = authOptions;
    }
    
    public async Task<SuccessLoginResponseDto> Register(RegisterRequestDto requestDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(requestDto.Email);
        if (user is not null)
        {
            throw new EmailIsExisting("Данный email уже занят");
        }
        
        user = await _userRepository.AddUserAsync(requestDto.MapDtoToDb());
        return new SuccessLoginResponseDto(AccessToken: GenerateToken(user.Id, user.Email, user.Role), CreatedAt: DateTime.Now);
    }

    public async Task<SuccessLoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginRequestDto.Email);
        if (user is null)
        {
            throw new InvalidLoginException();
        }

        if (!HashPassword.VerifyPassword(user.HashedPassword, loginRequestDto.Password))
        {
            throw new InvalidLoginException();
        }
        return new SuccessLoginResponseDto(GenerateToken(user.Id, user.Email, user.Role), DateTime.Now);
    }

    private IEnumerable<Claim> GetClaims(int userId, string email, Roles role)
    {
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email.ToString(), email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()),
            new Claim(ClaimTypes.UserId.ToString(), userId.ToString())
        };
        ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity.Claims;
    }
    
    private string GenerateToken(int userId, string email, Roles role)
    {
        var jwt = new JwtSecurityToken(
            issuer: _authOptions.ISSUER,
            audience: _authOptions.AUDIENCE,
            notBefore: DateTime.UtcNow,
            claims: GetClaims(userId, email, role),
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.ACCESS_LIFETINE_IN_MINUTE)),
            signingCredentials: new SigningCredentials(
                _authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
}