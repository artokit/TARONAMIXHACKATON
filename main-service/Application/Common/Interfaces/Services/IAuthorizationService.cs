using Contracts.Authorization;
using Contracts.Authorization.Requests;
using Contracts.Authorization.Responses;

namespace Application.Common.Interfaces.Services;

public interface IAuthorizationService
{
    public Task<SuccessLoginResponseDto> Register(RegisterRequestDto requestDto);
    public Task<SuccessLoginResponseDto> Login(LoginRequestDto loginRequestDto);
}