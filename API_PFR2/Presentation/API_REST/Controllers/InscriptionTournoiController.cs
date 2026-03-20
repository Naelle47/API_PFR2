using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
namespace API_PFR2.Presentation.API_REST.Controllers;

/// <summary>
/// Provides endpoints for managing tournament registrations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InscriptionTournoiController : APIBaseController
{
    private readonly IInscriptionTournoiService _inscriptionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InscriptionTournoiController"/> class.
    /// </summary>
    /// <param name="inscriptionService">Service responsible for registration-related business operations.</param>
    public InscriptionTournoiController(IInscriptionTournoiService inscriptionService)
    {
        _inscriptionService = inscriptionService;
    }

    /// <summary>
    /// Retrieves all registrations for a specific tournament.
    /// </summary>
    /// <param name="tournoiId">The identifier of the tournament.</param>
    /// <returns>A collection of registrations for the specified tournament.</returns>
    /// <response code="200">Returns the list of registrations.</response>
    [HttpGet("tournoi/{tournoiId}")]
    public async Task<ActionResult<IEnumerable<InscriptionTournoiResponse>>> GetByTournoi(int tournoiId)
    {
        var inscriptions = await _inscriptionService.GetByTournoiIdAsync(tournoiId);
        var response = inscriptions.Select(i => new InscriptionTournoiResponse
        {
            Id = i.id,
            UtilisateurId = i.utilisateurId,
            TournoiId = i.tournoiId,
            Statut = i.statut.ToString(),
            DateInscription = i.dateInscription
        }).ToList();
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all registrations for a specific user.
    /// </summary>
    /// <param name="utilisateurId">The identifier of the user.</param>
    /// <returns>A collection of registrations for the specified user.</returns>
    /// <response code="200">Returns the list of registrations.</response>
    [HttpGet("utilisateur/{utilisateurId}")]
    public async Task<ActionResult<IEnumerable<InscriptionTournoiResponse>>> GetByUtilisateur(int utilisateurId)
    {
        var inscriptions = await _inscriptionService.GetByUtilisateurIdAsync(utilisateurId);
        var response = inscriptions.Select(i => new InscriptionTournoiResponse
        {
            Id = i.id,
            UtilisateurId = i.utilisateurId,
            TournoiId = i.tournoiId,
            Statut = i.statut.ToString(),
            DateInscription = i.dateInscription
        }).ToList();
        return Ok(response);
    }

    /// <summary>
    /// Registers a user for a tournament.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <returns>The identifier of the newly created registration.</returns>
    /// <response code="201">Registration successfully created.</response>
    /// <response code="409">The user is already registered for this tournament.</response>
    [HttpPost]
    public async Task<ActionResult<int>> Register([FromBody] CreateInscriptionTournoiRequest request)
    {
        int id = await _inscriptionService.RegisterAsync(request.UtilisateurId, request.TournoiId);
        return CreatedAtAction(nameof(GetByTournoi), new { tournoiId = request.TournoiId }, id);
    }
}