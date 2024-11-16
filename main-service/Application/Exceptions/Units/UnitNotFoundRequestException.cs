using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Units;

public class UnitNotFoundRequestException(string message = "Данного подразделения не существует") : NotFoundException(message);