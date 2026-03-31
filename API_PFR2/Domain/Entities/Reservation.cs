namespace API_PFR2.Domain.Entities;

/// <summary>
/// Represents a reservation of a game by a user for a specific time period.
/// </summary>
/// <remarks>
/// A reservation links a user to a game during a defined time slot.
/// It is used to manage scheduling and availability of games in the system.
/// </remarks>
public class Reservation
{
    /// <summary>
    /// Gets or sets the unique identifier of the reservation.
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the reservation.
    /// </summary>
    /// <remarks>
    /// This value indicates when the reservation begins.
    /// </remarks>
    public DateTime dateDebut { get; set; }

    /// <summary>
    /// Gets or sets the end date and time of the reservation.
    /// </summary>
    /// <remarks>
    /// This value must be later than <see cref="dateDebut"/>.
    /// </remarks>
    public DateTime dateFin { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who made the reservation.
    /// </summary>
    public int utilisateurId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reserved game.
    /// </summary>
    public int jeuId { get; set; }

    /// <summary>
    /// Gets or sets the email of the user who made the reservation.
    /// </summary>
    public string? EmailUtilisateur { get; set; }

    /// <summary>
    /// Gets or sets the game associated with the reservation.
    /// </summary>
    public  Jeu? jeu { get; set; }
}