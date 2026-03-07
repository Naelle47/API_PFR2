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
    /// <param name="dbConnection">
    /// The database connection used to execute queries.
    /// </param>
    public JeuRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Retrieves all games from the database.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Jeu"/> objects representing all games stored in the database.
    /// </returns>
    public IEnumerable<Jeu> GetAll()
    {
        string sql = @"SELECT id, nom, description 
                       FROM jeu";

        return _dbConnection.Query<Jeu>(sql);
    }

    /// <summary>
    /// Retrieves a game from the database using its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// The <see cref="Jeu"/> if it exists; otherwise <c>null</c>.
    /// </returns>
    public Jeu? GetById(int id)
    {
        string sql = @"SELECT id, nom, description 
                       FROM jeu
                       WHERE id = @Id";

        return _dbConnection.QueryFirstOrDefault<Jeu>(sql, new { Id = id });
    }

    /// <summary>
    /// Checks whether a game with the specified identifier exists in the database.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the game.
    /// </param>
    /// <returns>
    /// <c>true</c> if the game exists in the database; otherwise <c>false</c>.
    /// </returns>
    public bool Exists(int id)
    {
        string sql = @"SELECT COUNT(1)
                       FROM jeu
                       WHERE id = @Id";

        int count = _dbConnection.ExecuteScalar<int>(sql, new { Id = id });
        return count > 0;
    }
}
