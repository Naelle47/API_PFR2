using System.ComponentModel.DataAnnotations;

namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to create a reservation.
/// </summary>
public class CreateReservationRequest
{
    /// <summary>
    /// Gets or sets the identifier of the user making the reservation.
    /// </summary>
    [Required(ErrorMessage = "L'identifiant de l'utilisateur est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de l'utilisateur doit être supérieur à 0.")]
    public int UtilisateurId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reserved game.
    /// </summary>
    [Required(ErrorMessage = "L'identifiant du jeu est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'identifiant du jeu doit être supérieur à 0.")]
    public int JeuId { get; set; }

    /// <summary>
    /// Gets or sets the reservation start date.
    /// </summary>
    [Required(ErrorMessage = "La date de début est obligatoire.")]
    public DateTime DateDebut { get; set; }

    /// <summary>
    /// Gets or sets the reservation end date.
    /// </summary>
    [Required(ErrorMessage = "La date de fin est obligatoire.")]
    public DateTime DateFin { get; set; }
}