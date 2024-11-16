using Contracts.Users.Responses;

namespace Application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<GetUserResponseDto> GetMe(int userId);
}