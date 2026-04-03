using FluentValidation;

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

public class CreateTournoiRequestValidator : AbstractValidator<CreateTournoiRequest>
{
    public CreateTournoiRequestValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty()
            .WithMessage("Le nom du tournoi est obligatoire.")
            .Length(3, 100)
            .WithMessage("Le nom doit contenir entre 3 et 100 caractères.");

        RuleFor(x => x.JeuId)
            .GreaterThan(0)
            .WithMessage("L'identifiant du jeu doit être supérieur à 0.");

        RuleFor(x => x.Capacite)
            .InclusiveBetween(2, 100)
            .WithMessage("La capacité doit être comprise entre 2 et 100 participants.");

        RuleFor(x => x.DateDebut)
            .NotEmpty()
            .WithMessage("La date de début est obligatoire.");

        RuleFor(x => x.DateFin)
            .NotEmpty()
            .WithMessage("La date de fin est obligatoire.")
            .GreaterThan(x => x.DateDebut)
            .WithMessage("La date de fin doit être postérieure à la date de début.");
    }
}