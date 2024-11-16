using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Workers;

public class WorkerEmailIsExistException(string message = "Email работника уже занят") : BadRequestException(message);