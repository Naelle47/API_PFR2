using System.Data;
using API_PFR2.DAL.Implementations;
using API_PFR2.DAL.Interfaces;
namespace API_PFR2.DAL;

/// <summary>
/// Implements the Unit of Work pattern, coordinating multiple repository operations
/// within a single database transaction.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;

    /// <inheritdoc/>
    public IReservationRepository Reservations { get; }

     /// <inheritdoc/>
     public ITournoiRepository Tournois { get; }

     /// <inheritdoc/>
     public IInscriptionTournoiRepository InscriptionsTournoi { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="connection">The shared database connection.</param>
    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
        Reservations = new ReservationRepository(_connection);
        Tournois = new TournoiRepository(_connection);
        InscriptionsTournoi = new InscriptionTournoiRepository(_connection);
    }

    /// <inheritdoc/>
    public void BeginTransaction()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    /// <inheritdoc/>
    public async Task CommitAsync()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _transaction?.Dispose();
        _connection.Dispose();
    }
}