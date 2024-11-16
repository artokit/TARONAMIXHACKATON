using System.ComponentModel.DataAnnotations;

namespace Contracts.Authorization.Requests;

public class LoginRequestDto
{
    [EmailAddress(ErrorMessage = "Введите Email")]
    public string Email { get; set; }
    public string Password { get; set; }
}