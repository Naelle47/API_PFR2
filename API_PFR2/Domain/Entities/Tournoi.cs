namespace API_PFR2.Domain.Entities;

// Classe représentant un tournoi organisé dans l'application, avec des détails tels que le nom, la date, les participants, etc.

/// <summary>
/// Represents a tournament organized within the application, including details such as the name, start date, end date,
/// and participant capacity.
/// </summary>
/// <remarks>A tournament is associated with a specific game, identified by the game ID. Ensure that the
/// tournament's start date is before the end date and that the capacity is a positive integer.</remarks>
public class Tournoi
{
   /// <summary>
   /// Gets or sets the unique identifier for the entity.
   /// </summary>
   /// <remarks>The <see cref="id"/> property is typically used to uniquely identify an instance of the entity
   /// within a collection or database. It is important that the identifier is unique across all instances to avoid
   /// conflicts.</remarks>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the name associated with the entity.
    /// </summary>
    /// <remarks>This property can be null, indicating that no name has been assigned.</remarks>
    public string? nom { get; set; }
    /// <summary>
    /// Gets or sets the start date and time of the event.
    /// </summary>
    /// <remarks>This property specifies when the event begins. Set this value to a valid future date to avoid
    /// scheduling conflicts.</remarks>
    public DateTime dateDebut { get; set; }
    /// <summary>
    /// Gets or sets the end date and time for the event.
    /// </summary>
    /// <remarks>This property represents the final date and time when the event concludes. Ensure that the
    /// value assigned is not earlier than the start date of the event to maintain logical consistency.</remarks>
    public DateTime dateFin { get; set; }
    /// <summary>
    /// Gets or sets the capacity of the container, representing the maximum number of items it can hold.
    /// </summary>
    /// <remarks>The capacity value must be a non-negative integer. Setting the capacity to a value less than
    /// the current number of items will not reduce the number of items in the container.</remarks>
    public int capacité { get; set; }

    // Un tournoi est associé à un jeu.
    /// <summary>
    /// Gets or sets the identifier of the game associated with the tournament.
    /// </summary>
    /// <remarks>This property links a tournament to a specific game, allowing for the identification of the
    /// game involved in the tournament's context.</remarks>
    public int jeuId { get; set; }
}
