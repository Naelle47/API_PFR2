using System.ComponentModel.DataAnnotations;

namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the data required to authenticate a user.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password provided by the user.
    /// </summary>
    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    public required string Password { get; set; }
}