namespace API_PFR2.Presentation.API_REST.DTO.Responses;

/// <summary>
/// Represents the data returned for a tournament registration.
/// </summary>
public class InscriptionTournoiResponse
{
    /// <summary>Gets or sets the unique identifier of the registration.</summary>
    public int Id { get; set; }
    /// <summary>Gets or sets the identifier of the registered user.</summary>
    public int UtilisateurId { get; set; }
    /// <summary>Gets or sets the identifier of the tournament.</summary>
    public int TournoiId { get; set; }
    /// <summary>Gets or sets the current registration status.</summary>
    public string Statut { get; set; } = string.Empty;
    /// <summary>Gets or sets the registration date.</summary>
    public DateTime DateInscription { get; set; }
}