using API_PFR2.DAL.Interfaces;
namespace API_PFR2.DAL;

/// <summary>
/// Defines a unit of work that groups multiple repository operations
/// into a single atomic transaction.
/// </summary>
/// <remarks>
/// The Unit of Work pattern ensures that all operations within a transaction
/// either succeed or fail together, maintaining data consistency.
/// It provides access to repositories that share the same database connection
/// and transaction scope.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the reservation repository within the current transaction scope.
    /// </summary>
    IReservationRepository Reservations { get; }

    // /// <summary>
    // /// Gets the tournoi repository within the current transaction scope.
    // /// </summary>
    // ITournoiRepository Tournois { get; }

    // /// <summary>
    // /// Gets the inscription tournoi repository within the current transaction scope.
    // /// </summary>
    // IInscriptionTournoiRepository InscriptionsTournoi { get; }

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Commits the current transaction, persisting all changes to the database.
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Rolls back the current transaction, discarding all changes.
    /// </summary>
    void Rollback();
}