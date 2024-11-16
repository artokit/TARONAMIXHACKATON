using Application.Services.Common;
using Contracts.Authorization.Requests;
using Contracts.Users.Responses;
using Domain.Postgres;

namespace Application.Mappers;

public static class UserMapper
{
    public static DbUser MapDtoToDb(this RegisterRequestDto dto)
    {
        return new DbUser
        {
            Email = dto.Email,
            Name = dto.Name,
            Surname = dto.Surname,
            Patronymic = dto.Patronymic,
            Role = dto.Role,
            HashedPassword = HashPassword.GenerateHashPassword(dto.Password)
        };
    }

    public static GetUserResponseDto MapDbToDto(this DbUser dbUser)
    {
        return new GetUserResponseDto
        {
            Email = dbUser.Email,
            Id = dbUser.Id,
            Name = dbUser.Name,
            Patronymic = dbUser.Patronymic,
            Role = dbUser.Role,
            Surname = dbUser.Surname
        };
    }
}