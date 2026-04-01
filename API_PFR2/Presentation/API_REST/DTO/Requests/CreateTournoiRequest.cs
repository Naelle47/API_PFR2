using System.ComponentModel.DataAnnotations;

namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to create a new tournament.
/// </summary>
public class CreateTournoiRequest
{
    /// <summary>Gets or sets the name of the tournament.</summary>
    [Required(ErrorMessage = "Le nom du tournoi est obligatoire.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre 3 et 100 caractères.")]
    public string? Nom { get; set; }

    /// <summary>Gets or sets the start date of the tournament.</summary>
    [Required(ErrorMessage = "La date de début est obligatoire.")]
    public DateTime DateDebut { get; set; }

    /// <summary>Gets or sets the end date of the tournament.</summary>
    [Required(ErrorMessage = "La date de fin est obligatoire.")]
    public DateTime DateFin { get; set; }

    /// <summary>Gets or sets the maximum number of participants.</summary>
    [Required(ErrorMessage = "La capacité est obligatoire.")]
    [Range(2, 100, ErrorMessage = "La capacité doit être comprise entre 2 et 100 participants.")]
    public int Capacite { get; set; }

    /// <summary>Gets or sets the identifier of the game associated with the tournament.</summary>
    [Required(ErrorMessage = "L'identifiant du jeu est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant du jeu doit être supérieur à 0.")]
    public int JeuId { get; set; }
}