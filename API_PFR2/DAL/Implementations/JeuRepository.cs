using System.Data;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using Dapper;

namespace API_PFR2.DAL.Implementations;


/// <summary>
/// 
/// </summary>
public class JeuRepository : IJeuRepository
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbConnection"></param>
    public JeuRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Jeu> GetAll()
    {
        string sql = @"SELECT id, nom, description 
                       FROM jeu";

        return _dbConnection.Query<Jeu>(sql);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Jeu? GetById(int id)
    {
        string sql = @"SELECT id, nom, description 
                       FROM jeu
                       WHERE id = @Id";

        return _dbConnection.QueryFirstOrDefault<Jeu>(sql, new { Id = id });
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Exists(int id)
    {
        string sql = @"SELECT COUNT(1)
                       FROM jeu
                       WHERE id = @Id";

        int count = _dbConnection.ExecuteScalar<int>(sql, new { Id = id });
        return count > 0;
    }
}
