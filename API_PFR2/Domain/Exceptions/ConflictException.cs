namespace API_PFR2.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a conflict is detected,
/// such as a duplicate reservation or a tournament at full capacity.
/// </summary>
public class ConflictException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class
    /// with the specified message.
    /// </summary>
    /// <param name="message">The message describing the conflict.</param>
    public ConflictException(string message)
        : base(message)
    {
    }
}