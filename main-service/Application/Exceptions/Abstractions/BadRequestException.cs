namespace Application.Exceptions.Abstractions;

public abstract class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
