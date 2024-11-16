using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Workers;

public class EqualsWorkerNotCompareException(string message = "Вы не можете сравнивать одного и того же работника") : BadRequestException(message);