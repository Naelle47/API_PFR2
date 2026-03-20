using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
namespace API_PFR2.Presentation.API_REST.Controllers;

/// <summary>
/// Provides endpoints for managing tournaments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TournoiController : APIBaseController
{
    private readonly ITournoiService _tournoiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournoiController"/> class.
    /// </summary>
    /// <param name="tournoiService">Service responsible for tournament-related business operations.</param>
    public TournoiController(ITournoiService tournoiService)
    {
        _tournoiService = tournoiService;
    }

    /// <summary>
    /// Retrieves all tournaments.
    /// </summary>
    /// <returns>A collection of <see cref="TournoiResponse"/> representing all tournaments.</returns>
    /// <response code="200">Returns the list of tournaments.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournoiResponse>>> GetAll()
    {
        var tournois = await _tournoiService.GetAllAsync();
        var response = tournois.Select(t => new TournoiResponse
        {
            Id = t.id,
            Nom = t.nom,
            DateDebut = t.dateDebut,
            DateFin = t.dateFin,
            Capacite = t.capacite,
            JeuId = t.jeuId
        }).ToList();
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific tournament by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament.</param>
    /// <returns>A <see cref="TournoiResponse"/> representing the requested tournament.</returns>
    /// <response code="200">Returns the requested tournament.</response>
    /// <response code="404">If the tournament does not exist.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TournoiResponse>> GetById(int id)
    {
        var tournoi = await _tournoiService.GetByIdAsync(id);
        if (tournoi == null)
            return NotFound($"Tournament with id {id} was not found.");

        return Ok(new TournoiResponse
        {
            Id = tournoi.id,
            Nom = tournoi.nom,
            DateDebut = tournoi.dateDebut,
            DateFin = tournoi.dateFin,
            Capacite = tournoi.capacite,
            JeuId = tournoi.jeuId
        });
    }

    /// <summary>
    /// Creates a new tournament.
    /// </summary>
    /// <param name="request">The tournament creation request.</param>
    /// <returns>The identifier of the newly created tournament.</returns>
    /// <response code="201">Tournament successfully created.</response>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateTournoiRequest request)
    {
        var tournoi = new Tournoi
        {
            nom = request.Nom,
            dateDebut = request.DateDebut,
            dateFin = request.DateFin,
            capacite = request.Capacite,
            jeuId = request.JeuId
        };

        int id = await _tournoiService.CreateAsync(tournoi);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Cancels a tournament and notifies affected users.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament to cancel.</param>
    /// <response code="204">Tournament successfully cancelled.</response>
    /// <response code="404">If the tournament does not exist.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _tournoiService.CancelAsync(id);
        return NoContent();
    }
}