using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Authorization;

public class InvalidLoginException(string message = "Неверный логин/пароль") : BadRequestException(message);