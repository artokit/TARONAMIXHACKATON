using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Users;

public class UserCantBeRecruiterException(string message = "Данный пользователь не может быть рекрутером") : BadRequestException(message);
