namespace Application.Exceptions.Abstractions;

public class NotFoundException(string message) : Exception(message);
