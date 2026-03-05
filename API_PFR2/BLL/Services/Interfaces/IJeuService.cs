using API_PFR2.Domain.Entities;

namespace API_PFR2.BLL.Services.Interfaces;
/// <summary>
/// 
/// </summary>
public interface IJeuService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<Jeu> GetCatalogue();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Jeu? GetJeuById(int id);
}
