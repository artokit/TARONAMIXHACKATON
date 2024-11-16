using Application.Common.Interfaces.Persistence;
using Domain.Postgres;
using Infrastructure.Common.Scripts.Users;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Common.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private IDapperContext _dapperContext;

    public UserRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbUser> AddUserAsync(DbUser user)
    {
        var queryObject =
            new QueryObject(PostgresUserElement.CreateUser,
                new
                {
                    email=user.Email,
                    hashed_password=user.HashedPassword,
                    name=user.Name,
                    surname=user.Surname,
                    patronymic=user.Patronymic,
                    role=user.Role
                }
            );
        return await _dapperContext.CommandWithResponse<DbUser>(queryObject);
    }

    public async Task<DbUser?> GetUserByIdAsync(int id)
    {
        var queryObject =
            new QueryObject(PostgresUserElement.GetUserById,
                new { id }
            );
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }

    public async Task<DbUser?> GetUserByEmailAsync(string email)
    {
        var queryObject =
            new QueryObject(PostgresUserElement.GetUserByEmail,
                new { email }
            );
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }
}