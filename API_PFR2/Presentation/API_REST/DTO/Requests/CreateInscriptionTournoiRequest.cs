namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to register a user for a tournament.
/// </summary>
public class CreateInscriptionTournoiRequest
{
    /// <summary>Gets or sets the identifier of the user.</summary>
    public int UtilisateurId { get; set; }
    /// <summary>Gets or sets the identifier of the tournament.</summary>
    public int TournoiId { get; set; }
}