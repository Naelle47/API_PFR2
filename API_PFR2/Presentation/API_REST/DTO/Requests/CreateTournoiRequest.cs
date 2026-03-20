namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to create a new tournament.
/// </summary>
public class CreateTournoiRequest
{
    /// <summary>Gets or sets the name of the tournament.</summary>
    public string? Nom { get; set; }
    /// <summary>Gets or sets the start date of the tournament.</summary>
    public DateTime DateDebut { get; set; }
    /// <summary>Gets or sets the end date of the tournament.</summary>
    public DateTime DateFin { get; set; }
    /// <summary>Gets or sets the maximum number of participants.</summary>
    public int Capacite { get; set; }
    /// <summary>Gets or sets the identifier of the game associated with the tournament.</summary>
    public int JeuId { get; set; }
}