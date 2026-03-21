namespace API_PFR2.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a user attempts to access a resource
/// they do not have permission to access.
/// </summary>
public class ForbiddenAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenAccessException"/> class.
    /// </summary>
    public ForbiddenAccessException()
        : base("You do not have permission to access this resource.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenAccessException"/> class
    /// with a custom message.
    /// </summary>
    /// <param name="message">The message describing the access violation.</param>
    public ForbiddenAccessException(string message)
        : base(message)
    {
    }
}