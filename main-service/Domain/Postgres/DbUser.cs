using Domain.Common.Users;

namespace Domain.Postgres;

public class DbUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public Roles Role { get; set; }
}