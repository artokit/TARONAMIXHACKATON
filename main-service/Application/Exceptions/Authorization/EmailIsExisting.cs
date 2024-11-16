using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Authorization;

public class EmailIsExisting(string message) : BadRequestException(message);