using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides business logic operations related to <see cref="Jeu"/> entities.
/// </summary>
/// <remarks>
/// This service acts as an intermediary between the controllers and the data access layer,
/// using <see cref="IJeuRepository"/> to retrieve game data.
/// </remarks>
public class JeuService : IJeuService
{
    private readonly IJeuRepository _jeuRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="JeuService"/> class.
    /// </summary>
    /// <param name="jeuRepository">Repository used to access game data.</param>
    public JeuService(IJeuRepository jeuRepository)
    {
        _jeuRepository = jeuRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Jeu>> GetCatalogueAsync()
    {
        return await _jeuRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task<Jeu?> GetJeuByIdAsync(int id)
    {
        if (!await _jeuRepository.ExistsAsync(id))
            return null;
        return await _jeuRepository.GetByIdAsync(id);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(int id)
    {
        return await _jeuRepository.ExistsAsync(id);
    }
}