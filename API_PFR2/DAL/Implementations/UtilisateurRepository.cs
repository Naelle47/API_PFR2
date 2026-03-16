using System.Data;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using Dapper;
namespace API_PFR2.DAL.Implementations;

/// <summary>
/// Provides data access operations for <see cref="Utilisateur"/> entities.
/// </summary>
/// <remarks>
/// This repository implements <see cref="IUtilisateurRepository"/> and uses Dapper
/// to execute SQL queries against the database.
/// </remarks>
public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly IDbConnection _dbConnection;
    /// <summary>
    /// Initializes a new instance of the <see cref="UtilisateurRepository"/> class.
    /// </summary>
    /// <param name="dbConnection">The database connection used to execute queries.</param>
    /// <inheritdoc/>
    public UtilisateurRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Utilisateur?> GetByEmailAsync(string email)
    {
        string sql = @"SELECT id, email, password AS passwordHash, role
                       FROM api_users
                       WHERE email = @Email";
        return await _dbConnection.QueryFirstOrDefaultAsync<Utilisateur>(sql, new { Email = email });
    }

    public async Task<bool> ExistsAsync(string email)
    {
        string sql = @"SELECT COUNT(1) FROM api_users WHERE email = @Email";
        int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Email = email });
        return count > 0;
    }



}
