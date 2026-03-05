using API_PFR2.Domain.Entities;

namespace API_PFR2.DAL.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IReservationRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns></returns>
    int Add(Reservation reservation);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jeuId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    bool ExistsForGameAtDate(int jeuId, DateTime date);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="JeuId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    IEnumerable<Reservation> GetByNameAndDate(int JeuId, DateTime date);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jeuId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    int DeleteByGameAndDate(int jeuId, DateTime date);
}
