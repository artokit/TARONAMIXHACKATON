using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Workers;

public class WorkerNotFoundException(string message = "Данного работника нету в системе") : NotFoundException(message);