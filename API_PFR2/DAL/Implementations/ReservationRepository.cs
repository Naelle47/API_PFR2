using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using Dapper;
using System.Data;

namespace API_PFR2.DAL.Implementations;

/// <summary>
/// Provides data access operations for <see cref="Reservation"/> entities.
/// </summary>
/// <remarks>
/// ReservationRepository implements the <see cref="IReservationRepository"/> interface and uses Dapper to execute SQL queries against the database. 
/// It provides methods to add new reservations, check for existing reservations for a game at a specific date, retrieve reservations based on game and date,
/// and delete reservations when necessary (e.g., when a tournament takes priority). 
/// This repository serves as the data access layer for managing reservations in the application.
/// </remarks>
public class ReservationRepository : IReservationRepository
{
    private readonly IDbConnection _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection"></param>
    public ReservationRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Adds a reservation in database
    /// </summary>
    public int Add(Reservation reservation)
    {
        string sql = @"
            INSERT INTO Reservation (dateDebut, dateFin, utilisateurId, jeuId)
            VALUES (@dateDebut, @dateFin, @utilisateurId, @jeuId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

        return _connection.ExecuteScalar<int>(sql, reservation);
    }

    /// <summary>
    /// Check if a reservation already exists for a game at a given date
    /// </summary>
    public bool ExistsForGameAtDate(int jeuId, DateTime date)
    {
        string sql = @"
            SELECT COUNT(*)
            FROM Reservation
            WHERE jeuId = @jeuId
            AND @date BETWEEN dateDebut AND dateFin
        ";

        int count = _connection.ExecuteScalar<int>(sql, new { jeuId, date });
        return count > 0;
    }

    /// <summary>
    /// Returns reservations for a game at a given date
    /// </summary>
    public IEnumerable<Reservation> GetByGameAndDate(int jeuId, DateTime date)
    {
        string sql = @"
            SELECT *
            FROM Reservation
            WHERE jeuId = @jeuId
            AND @date BETWEEN dateDebut AND dateFin
        ";

        return _connection.Query<Reservation>(sql, new { jeuId, date });
    }

    /// <summary>
    /// Deletes reservations for a game at a given date
    /// </summary>
    public int DeleteByGameAndDate(int jeuId, DateTime date)
    {
        string sql = @"
            DELETE FROM Reservation
            WHERE jeuId = @jeuId
            AND @date BETWEEN dateDebut AND dateFin
        ";

        return _connection.Execute(sql, new { jeuId, date });
    }
}