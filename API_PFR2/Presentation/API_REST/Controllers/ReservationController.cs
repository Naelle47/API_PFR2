using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.Domain.Entities;
using API_PFR2.Domain.Exceptions;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace API_PFR2.Presentation.API_REST.Controllers;

/// <summary>
/// Provides endpoints for managing game reservations.
/// </summary>
/// <remarks>
/// This controller exposes REST API endpoints allowing authenticated users
/// to create, retrieve and cancel reservations.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ReservationController : APIBaseController
{
    private readonly IReservationService _reservationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReservationController"/> class.
    /// </summary>
    /// <param name="reservationService">Service responsible for reservation-related business operations.</param>
    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /// <summary>
    /// Creates a new reservation for a game.
    /// </summary>
    /// <param name="request">The reservation request containing game id and dates.</param>
    /// <returns>The identifier of the newly created reservation.</returns>
    /// <response code="201">Reservation successfully created.</response>
    /// <response code="409">The game is already reserved for the selected date.</response>
    [HttpPost]
    public async Task<ActionResult<int>> CreateReservation([FromBody] CreateReservationRequest request)
    {

            var reservation = new Reservation
            {
                jeuId = request.JeuId,
                utilisateurId = request.UtilisateurId,
                dateDebut = request.DateDebut,
                dateFin = request.DateFin
            };

            int id = await _reservationService.CreateReservationAsync(reservation);
            return Created($"/api/reservation/{id}", id);
    }

    /// <summary>
    /// Retrieves reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date to filter reservations.</param>
    /// <returns>A collection of reservations matching the criteria.</returns>
    /// <response code="200">Returns the matching reservations.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> GetByGameAndDate(
        [FromQuery] int jeuId,
        [FromQuery] DateTime date)
    {
        var reservations = await _reservationService.GetReservationsByGameAndDateAsync(jeuId, date);
        var response = reservations.Select(r => new ReservationResponse
        {
            Id = r.id,
            JeuId = r.jeuId,
            UtilisateurId = r.utilisateurId,
            EmailUtilisateur = r.EmailUtilisateur,
            DateDebut = r.dateDebut,
            DateFin = r.dateFin
        }).ToList();
        return Ok(response);
    }

    /// <summary>
    /// Cancels reservations for a specific game at a given date.
    /// </summary>
    /// <param name="jeuId">The identifier of the game.</param>
    /// <param name="date">The date of the reservations to cancel.</param>
    /// <returns>The number of reservations cancelled.</returns>
    /// <response code="200">Returns the number of cancelled reservations.</response>
    [HttpDelete]
    public async Task<ActionResult<int>> CancelReservation(
        [FromQuery] int jeuId,
        [FromQuery] DateTime date)
    {
        int count = await _reservationService.CancelReservationAsync(jeuId, date);
        return Ok(count);
    }
}