using System.ComponentModel.DataAnnotations;
using Domain.Common.Users;

namespace Contracts.Authorization.Requests;

public class RegisterRequestDto
{
    [EmailAddress(ErrorMessage = "Введите Email")]
    public string Email { get; set; }

    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public string? Patronymic { get; set; }
    public Roles Role { get; set; }
}