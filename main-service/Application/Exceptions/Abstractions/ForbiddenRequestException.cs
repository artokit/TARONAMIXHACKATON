namespace Application.Exceptions.Abstractions;

public abstract class ForbiddenRequestException : Exception
{
    public ForbiddenRequestException(string message) : base(message) { }
}