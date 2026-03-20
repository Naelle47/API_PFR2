using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Enums;
namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides business logic operations related to <see cref="InscriptionTournoi"/> entities.
/// </summary>
/// <remarks>
/// This service manages tournament registrations, enforcing business rules such as
/// preventing duplicate registrations.
/// </remarks>
public class InscriptionTournoiService : IInscriptionTournoiService
{
    private readonly IInscriptionTournoiRepository _inscriptionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="InscriptionTournoiService"/> class.
    /// </summary>
    /// <param name="inscriptionRepository">Repository used to access registration data.</param>
    public InscriptionTournoiService(IInscriptionTournoiRepository inscriptionRepository)
    {
        _inscriptionRepository = inscriptionRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InscriptionTournoi>> GetByTournoiIdAsync(int tournoiId)
    {
        return await _inscriptionRepository.GetByTournoiIdAsync(tournoiId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InscriptionTournoi>> GetByUtilisateurIdAsync(int utilisateurId)
    {
        return await _inscriptionRepository.GetByUtilisateurIdAsync(utilisateurId);
    }

    /// <inheritdoc/>
    public async Task<int> RegisterAsync(int utilisateurId, int tournoiId)
    {
        bool alreadyRegistered = await _inscriptionRepository.ExistsAsync(utilisateurId, tournoiId);
        if (alreadyRegistered)
            throw new InvalidOperationException("User is already registered for this tournament.");

        var inscription = new InscriptionTournoi
        {
            utilisateurId = utilisateurId,
            tournoiId = tournoiId,
            statut = StatutInscription.EnAttente,
            dateInscription = DateTime.UtcNow
        };

        return await _inscriptionRepository.AddAsync(inscription);
    }

    /// <inheritdoc/>
    public async Task<int> CancelByTournoiIdAsync(int tournoiId)
    {
        return await _inscriptionRepository.DeleteByTournoiIdAsync(tournoiId);
    }
}