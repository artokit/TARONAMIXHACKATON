using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Users;

public class UserNotFoundException(string message = "Пользователя с данным id нету в системе") : NotFoundException(message);