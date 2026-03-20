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
/// preventing duplicate registrations and ensuring tournament capacity is not exceeded.
/// </remarks>
public class InscriptionTournoiService : IInscriptionTournoiService
{
    private readonly IInscriptionTournoiRepository _inscriptionRepository;
    private readonly ITournoiRepository _tournoiRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="InscriptionTournoiService"/> class.
    /// </summary>
    /// <param name="inscriptionRepository">Repository used to access registration data.</param>
    /// <param name="tournoiRepository">Repository used to access tournament data.</param>
    public InscriptionTournoiService(
        IInscriptionTournoiRepository inscriptionRepository,
        ITournoiRepository tournoiRepository)
    {
        _inscriptionRepository = inscriptionRepository;
        _tournoiRepository = tournoiRepository;
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
        // Vérifier si l'utilisateur est déjà inscrit
        bool alreadyRegistered = await _inscriptionRepository.ExistsAsync(utilisateurId, tournoiId);
        if (alreadyRegistered)
            throw new InvalidOperationException("User is already registered for this tournament.");

        // Vérifier si le tournoi existe et si la capacité est atteinte
        var tournoi = await _tournoiRepository.GetByIdAsync(tournoiId);
        if (tournoi == null)
            throw new InvalidOperationException($"Tournament with id {tournoiId} was not found.");

        int inscriptionCount = await _tournoiRepository.CountInscriptionsAsync(tournoiId);
        if (inscriptionCount >= tournoi.capacite)
            throw new InvalidOperationException("Tournament is at full capacity.");

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