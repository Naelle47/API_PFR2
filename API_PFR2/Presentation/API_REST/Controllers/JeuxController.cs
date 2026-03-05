using API_PFR2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using API_PFR2.BLL.Services.Interfaces; 
using API_PFR2.Presentation.API_REST.DTO.Responses;


namespace API_PFR2.Presentation.API_REST.Controllers;

/// <summary>
/// Provides endpoints for retrieving games available in the catalogue.
/// </summary>
/// <remarks>
/// This controller exposes REST API endpoints allowing clients to retrieve
/// information about available <see cref="Jeu"/> entities.
/// </remarks>

[ApiController]
[Route("api/[controller]")]
public class JeuxController : APIBaseController
{
    private readonly IJeuService _jeuService;

    /// <summary>
    /// Initializes a new instance of the <see cref="JeuxController"/> class.
    /// </summary>
    /// <param name="jeuService">Service responsible for game-related business operations.</param>
    public JeuxController(IJeuService jeuService)
    {
        _jeuService = jeuService;
    }

    /// <summary>
    /// Retrieves the complete catalogue of games.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="JeuResponse"/> representing the available games.
    /// </returns>
    /// <response code="200">Returns the list of games.</response>
    [HttpGet]
    public ActionResult<IEnumerable<JeuResponse>> GetAllJeux()
    {
        var jeux = _jeuService.GetCatalogue()
        .Select(j => new JeuResponse
        {
            Id = j.id,
            Nom = j.nom
        }).ToList();
        return Ok(jeux);
    }

    /// <summary>
    /// Retrieves a specific game by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>
    /// A <see cref="JeuResponse"/> representing the requested game.
    /// </returns>
    /// <response code="200">Returns the requested game.</response>
    /// <response code="404">If the game does not exist.</response>
    [HttpGet("{id}")]
    public ActionResult<JeuResponse> GetById(int id)
    {
        // Récupère le jeu directement
        var jeu = _jeuService.GetJeuById(id);

        // Vérifie s'il existe
        if (jeu == null)
            return NotFound($"Le jeu avec l'id {id} n'a pas été trouvé.");

        // Crée la réponse
        var response = new JeuResponse
        {
            Id = jeu.id,
            Nom = jeu.nom
        };

        return Ok(response);
    }

}
