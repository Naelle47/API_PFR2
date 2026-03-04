namespace API_PFR2.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested entity with a specified identifier cannot be found in the
/// data store.
/// </summary>
/// <remarks>This exception is typically used to indicate that an operation failed because the target entity does
/// not exist. The exception message includes the type of entity and its identifier to assist in diagnosing the
/// issue.</remarks>
public class NotFoundEntityException : Exception
{
    /// <summary>
    /// Initializes a new instance of the NotFoundEntityException class with the specified entity type and identifier.
    /// </summary>
    /// <remarks>This exception is typically thrown when an attempt to retrieve an entity by its identifier
    /// fails because the entity does not exist in the data store.</remarks>
    /// <param name="entityName">The name of the entity type that could not be found.</param>
    /// <param name="id">The identifier of the entity that was not found.</param>
    public NotFoundEntityException(string entityName, int id) 
        : base($"Entity of type {entityName} with id {id} was not found.")   
    {
    }
}
