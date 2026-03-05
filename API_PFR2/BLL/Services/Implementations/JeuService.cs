using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;

namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// 
/// </summary>
public class JeuService : IJeuService
{
    private readonly IJeuRepository _jeuRepository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jeuRepository"></param>
    public JeuService(IJeuRepository jeuRepository)
    {
        _jeuRepository = jeuRepository;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Jeu> GetCatalogue()
    {
        return _jeuRepository.GetAll();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Jeu? GetJeuById(int id)
    {
        return _jeuRepository.GetById(id);
    }
}
