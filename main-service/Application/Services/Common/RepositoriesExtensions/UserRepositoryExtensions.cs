using Application.Common.Interfaces.Persistence;
using Application.Exceptions.Users;
using Domain.Postgres;

namespace Application.Services.Common.RepositoriesExtensions;

public static class UserRepositoryExtensions
{
    public static async Task<DbUser> GetUserByIdOrThrowAsync(this IUserRepository userRepository, int userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }
}