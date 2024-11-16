using Domain.Postgres;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public Task<DbUser> AddUserAsync(DbUser user);
    public Task<DbUser?> GetUserByIdAsync(int id);
    public Task<DbUser?> GetUserByEmailAsync(string email);
}