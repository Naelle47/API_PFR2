using System.Data;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using Dapper;
namespace API_PFR2.DAL.Implementations;

/// <summary>
/// Provides data access operations for <see cref="Jeu"/> entities.
/// </summary>
/// <remarks>
/// This repository implements <see cref="IJeuRepository"/> and uses Dapper
/// to execute SQL queries against the database in order to retrieve information about games.
/// </remarks>
public class JeuRepository : IJeuRepository
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="JeuRepository"/> class.
    /// </summary>
    /// <param name="dbConnection">The database connection used to execute queries.</param>
    public JeuRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Retrieves all games from the database.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation, containing a collection of <see cref="Jeu"/> objects.
    /// </returns>
    public async Task<IEnumerable<Jeu>> GetAllAsync()
    {
        string sql = @"SELECT id, nom, description 
                       FROM api_games";
        return await _dbConnection.QueryAsync<Jeu>(sql);
    }

    /// <summary>
    /// Retrieves a game from the database using its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the <see cref="Jeu"/> if found; otherwise <c>null</c>.
    /// </returns>
    public async Task<Jeu?> GetByIdAsync(int id)
    {
        string sql = @"SELECT id, nom, description 
                       FROM api_games
                       WHERE id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Jeu>(sql, new { Id = id });
    }

    /// <summary>
    /// Checks whether a game with the specified identifier exists in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <c>true</c> if the game exists; otherwise <c>false</c>.
    /// </returns>
    public async Task<bool> ExistsAsync(int id)
    {
        string sql = @"SELECT COUNT(1)
                       FROM api_game
                       WHERE id = @Id";
        int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }
}