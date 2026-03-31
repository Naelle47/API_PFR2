namespace API_PFR2.Presentation.API_REST.DTO.Responses;

/// <summary>
/// Represents reservation data returned by the API.
/// </summary>
public class ReservationResponse
{
    /// <summary>
    /// Gets or sets the reservation identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user.
    /// </summary>
    public int UtilisateurId { get; set; }

    /// <summary>
    /// Gets or sets the email of the user who made the reservation.
    /// </summary>
    public string? EmailUtilisateur { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reserved game.
    /// </summary>
    public int JeuId { get; set; }

    /// <summary>
    /// Gets or sets the reservation start date.
    /// </summary>
    public DateTime DateDebut { get; set; }

    /// <summary>
    /// Gets or sets the reservation end date.
    /// </summary>
    public DateTime DateFin { get; set; }
}
