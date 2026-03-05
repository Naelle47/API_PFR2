namespace API_PFR2.Domain.Entities;

// Classe représentant un jeu disponible à la réservation ou utilisé dans un tournoi.
// Les proprétés inclusent sont minimales pour identifier le jeu, son nom et une description.

/// <summary>
/// Represents a game that can be reserved or used in a tournament, providing essential properties to identify the game,
/// its name, and a description.
/// </summary>
/// <remarks>This class offers a minimal representation suitable for gaming applications or tournament management
/// systems. It is intended for scenarios where only basic game identification and descriptive information are
/// required.</remarks>
public class Jeu
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    /// <remarks>This property is typically used to distinguish between different instances of the entity in a
    /// collection or database.</remarks>
    public int id { get; set; }
    /// <summary>
    /// Gets or sets the name associated with the entity.
    /// </summary>
    public required string nom { get; set; }
    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    public string? description { get; set; }
}
