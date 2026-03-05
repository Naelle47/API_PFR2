namespace API_PFR2.Domain.Entities;

// Classe représentant un jeu disponible à la réservation ou utilisé dans un tournoi.
// Les proprétés inclusent sont minimales pour identifier le jeu, son nom et une description.

/// <summary>
/// Represents a game available in the system.
/// A game can be used for reservations or tournaments.
/// </summary>
public class Jeu
{
    /// <summary>
    /// Gets or sets the unique identifier of the game.
    /// </summary>
    public int id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the game.
    /// </summary>
    public required string nom { get; set; }

    /// <summary>
    /// Gets or sets the description of the game.
    /// This value can be null if no description is provided.
    /// </summary>
    public string? description { get; set; }
}
