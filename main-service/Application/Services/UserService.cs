using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Mappers;
using Contracts.Users.Responses;

namespace Application.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<GetUserResponseDto> GetMe(int userId)
    {
        return ((await _userRepository.GetUserByIdAsync(userId))!).MapDbToDto();
    }
}