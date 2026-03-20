namespace API_PFR2.Presentation.API_REST.DTO.Responses;

/// <summary>
/// Represents the data returned for a tournament.
/// </summary>
public class TournoiResponse
{
    /// <summary>Gets or sets the unique identifier of the tournament.</summary>
    public int Id { get; set; }
    /// <summary>Gets or sets the name of the tournament.</summary>
    public string? Nom { get; set; }
    /// <summary>Gets or sets the start date of the tournament.</summary>
    public DateTime DateDebut { get; set; }
    /// <summary>Gets or sets the end date of the tournament.</summary>
    public DateTime DateFin { get; set; }
    /// <summary>Gets or sets the maximum number of participants.</summary>
    public int Capacite { get; set; }
    /// <summary>Gets or sets the identifier of the associated game.</summary>
    public int JeuId { get; set; }
}