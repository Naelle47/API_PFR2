namespace PFR2_API.Domain.Entities;

// Classe représentant une réservation d’un jeu par un utilisateur sur une plage horaire.
/// <summary>
/// Represents a reservation of a game by a user for a specific time period.
/// </summary>
/// <remarks>A Reservation associates a user and a game with a defined start and end date, indicating which user
/// has reserved which game during the specified time slot. This entity is typically used to manage scheduling and
/// availability in systems that track game reservations.</remarks>
public class Reservation
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    /// <remarks>The <c>id</c> property is typically used to uniquely identify an instance of the entity
    /// within a collection or database. It is important that the <c>id</c> is unique to avoid conflicts.</remarks>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the start date and time of the event.
    /// </summary>
    /// <remarks>This property specifies when the event begins. Ensure that the value assigned is a valid
    /// DateTime representing the intended start moment.</remarks>
    public DateTime dateDebut { get; set; }
    /// <summary>
    /// Gets or sets the end date and time for the event.
    /// </summary>
    /// <remarks>This property represents the final date and time when the event concludes. Ensure that the
    /// value assigned is later than the start date to maintain a valid event duration.</remarks>
    public DateTime dateFin { get; set; }

    // Une réservation est associée à un utilisateur et un jeu.
    public int utilisateurId { get; set; }
    public int jeuId { get; set; }

    // Propriétés de navigation pour les relations avec les autres entités.
    /// <summary>
    /// Gets or sets the user associated with this entity. This property represents a navigation relationship to the
    /// related user entity.
    /// </summary>
    public required Utilisateur Utilisateur { get; set; }
    /// <summary>
    /// Gets or sets the game associated with this reservation.
    /// </summary>
    /// <remarks>This property is required and must be set when creating a reservation. The assigned value
    /// should be a valid instance of the game to which the reservation applies.</remarks>
    public required Jeu Jeu { get; set; }
}
