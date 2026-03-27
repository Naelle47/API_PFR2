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
    /// Initializes a new instance of the <see cref="ReservationRepository"/> class.
    /// </summary>
    /// <param name="connection">The database connection.</param>
    public ReservationRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <inheritdoc/>
    public async Task<int> AddAsync(Reservation reservation)
    {
        string sql = @"
            INSERT INTO reservation (date_debut, date_fin, utilisateur_id, jeu_id)
            VALUES (@dateDebut, @dateFin, @utilisateurId, @jeuId)
            RETURNING id;
        ";
        return await _connection.QuerySingleAsync<int>(sql, reservation);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsForGameAtDateAsync(int jeuId, DateTime date)
    {
        string sql = @"
            SELECT COUNT(*)
            FROM reservation
            WHERE jeu_id = @jeuId
            AND @date BETWEEN date_debut AND date_fin
        ";
        int count = await _connection.ExecuteScalarAsync<int>(sql, new { jeuId, date });
        return count > 0;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Reservation>> GetByGameAndDateAsync(int jeuId, DateTime date)
    {
        string sql = @"
            SELECT *
            FROM reservation
            WHERE jeu_id = @jeuId
            AND DATE(date_debut) = DATE(@date)
        ";
        return await _connection.QueryAsync<Reservation>(sql, new { jeuId, date });
    }

    /// <inheritdoc/>
    public async Task<int> DeleteByGameAndDateAsync(int jeuId, DateTime date)
    {
        string sql = @"
        DELETE FROM reservation
        WHERE jeu_id = @jeuId
        AND DATE(date_debut) = DATE(@date)
    ";
        return await _connection.ExecuteAsync(sql, new { jeuId, date });
    }
}