namespace Ordering.Application.Exceptions;

// public class NotFoundException(string name, object key) : ApplicationException($"Entity \"{name}\" ({key}) was not found.")
public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
