using API_PFR2.Domain.Entities;

namespace API_PFR2.DAL.Interfaces;


/// <summary>
/// Represents the contract for a repository that manages data access operations related to "Jeu" entities in the application.
/// It's responsible for defining methods for retrieving, adding, updating, and deleting "Jeu" records from the underlying data source,
/// Read only if necessary, and any other specific queries related to "Jeu" entities. This interface abstracts the data access layer, allowing for flexibility in implementation and promoting separation of concerns within the application architecture.
/// </summary>
public interface IJeuRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<Jeu> GetAll();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Jeu? GetById(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Exists(int id);

}
