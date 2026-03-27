
namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to create a reservation.
/// </summary>
public class CreateReservationRequest
{
    /// <summary>
    /// Gets or sets the identifier of the user making the reservation.
    /// </summary>
    public int? UtilisateurId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reserved game.
    /// </summary>
    public int? JeuId { get; set; }

    /// <summary>
    /// Gets or sets the reservation start date.
    /// </summary>
    public DateTime? DateDebut { get; set; }

    /// <summary>
    /// Gets or sets the reservation end date.
    /// </summary>
    public DateTime? DateFin { get; set; }
}
