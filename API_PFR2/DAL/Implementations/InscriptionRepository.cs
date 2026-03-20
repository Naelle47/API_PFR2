using System.Data;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Enums;
using Dapper;
namespace API_PFR2.DAL.Implementations;

/// <summary>
/// Provides data access operations for <see cref="InscriptionTournoi"/> entities.
/// </summary>
/// <remarks>
/// This repository implements <see cref="IInscriptionTournoiRepository"/> and uses Dapper
/// to execute SQL queries against the database.
/// </remarks>
public class InscriptionTournoiRepository : IInscriptionTournoiRepository
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="InscriptionTournoiRepository"/> class.
    /// </summary>
    /// <param name="dbConnection">The database connection used to execute queries.</param>
    public InscriptionTournoiRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InscriptionTournoi>> GetByTournoiIdAsync(int tournoiId)
    {
        string sql = @"SELECT id, date_inscription AS dateInscription, statut,
                              utilisateur_id AS utilisateurId, tournoi_id AS tournoiId
                       FROM inscriptiontournoi
                       WHERE tournoi_id = @tournoiId";
        return await _dbConnection.QueryAsync<InscriptionTournoi>(sql, new { tournoiId });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InscriptionTournoi>> GetByUtilisateurIdAsync(int utilisateurId)
    {
        string sql = @"SELECT id, date_inscription AS dateInscription, statut,
                              utilisateur_id AS utilisateurId, tournoi_id AS tournoiId
                       FROM inscriptiontournoi
                       WHERE utilisateur_id = @utilisateurId";
        return await _dbConnection.QueryAsync<InscriptionTournoi>(sql, new { utilisateurId });
    }

    /// <inheritdoc/>
    public async Task<int> AddAsync(InscriptionTournoi inscription)
    {
        string sql = @"INSERT INTO inscriptiontournoi (utilisateur_id, tournoi_id, statut, date_inscription)
                       VALUES (@utilisateurId, @tournoiId, @statut, @dateInscription)
                       RETURNING id;";
        return await _dbConnection.QuerySingleAsync<int>(sql, inscription);
    }

    /// <inheritdoc/>
    public async Task<int> DeleteByTournoiIdAsync(int tournoiId)
    {
        string sql = @"DELETE FROM inscriptiontournoi WHERE tournoi_id = @tournoiId";
        return await _dbConnection.ExecuteAsync(sql, new { tournoiId });
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(int utilisateurId, int tournoiId)
    {
        string sql = @"SELECT COUNT(1)
                       FROM inscriptiontournoi
                       WHERE utilisateur_id = @utilisateurId
                       AND tournoi_id = @tournoiId";
        int count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { utilisateurId, tournoiId });
        return count > 0;
    }
}