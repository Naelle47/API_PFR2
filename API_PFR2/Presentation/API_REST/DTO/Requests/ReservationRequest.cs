using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API_PFR2.Presentation.API_REST.DTO.Requests;

public class CreateReservationRequest
{
    public int UtilisateurId { get; set; }
    public int JeuId { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
}

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(x => x.UtilisateurId)
            .GreaterThan(0)
            .WithMessage("L'identifiant de l'utilisateur doit être supérieur à 0.");

        RuleFor(x => x.JeuId)
            .GreaterThan(0)
            .WithMessage("L'identifiant du jeu doit être supérieur à 0.");

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