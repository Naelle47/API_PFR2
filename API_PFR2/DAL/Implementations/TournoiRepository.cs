using System.Data;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using Dapper;
namespace API_PFR2.DAL.Implementations;

/// <summary>
/// Provides data access operations for <see cref="Tournoi"/> entities.
/// </summary>
/// <remarks>
/// This repository implements <see cref="ITournoiRepository"/> and uses Dapper
/// to execute SQL queries against the database.
/// </remarks>
public class TournoiRepository : ITournoiRepository
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournoiRepository"/> class.
    /// </summary>
    /// <param name="dbConnection">The database connection used to execute queries.</param>
    public TournoiRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Tournoi>> GetAllAsync()
    {
        string sql = @"SELECT id, nom, date_debut AS dateDebut, date_fin AS dateFin, 
                              capacite, jeu_id AS jeuId
                       FROM tournoi";
        return await _dbConnection.QueryAsync<Tournoi>(sql);
    }

    /// <inheritdoc/>
    public async Task<Tournoi?> GetByIdAsync(int id)
    {
        string sql = @"SELECT id, nom, date_debut AS dateDebut, date_fin AS dateFin,
                              capacite, jeu_id AS jeuId
                       FROM tournoi
                       WHERE id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Tournoi>(sql, new { Id = id });
    }

    /// <inheritdoc/>
    public async Task<int> AddAsync(Tournoi tournoi)
    {
        string sql = @"INSERT INTO tournoi (nom, date_debut, date_fin, capacite, jeu_id)
                       VALUES (@nom, @dateDebut, @dateFin, @capacite, @jeuId)
                       RETURNING id;";
        return await _dbConnection.QuerySingleAsync<int>(sql, tournoi);
    }

    /// <inheritdoc/>
    public async Task<int> DeleteAsync(int id)
    {
        string sql = @"DELETE FROM tournoi WHERE id = @Id";
        return await _dbConnection.ExecuteAsync(sql, new { Id = id });
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsForGameAtDateAsync(int jeuId, DateTime date)
    {
        string sql = @"SELECT COUNT(1)
                       FROM tournoi
                       WHERE jeu_id = @jeuId
                       AND @date BETWEEN date_debut AND date_fin";
        int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { jeuId, date });
        return count > 0;
    }
}