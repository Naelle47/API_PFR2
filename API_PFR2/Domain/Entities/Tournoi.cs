namespace PFR2_API.Domain.Entities;

// Classe représentant un tournoi organisé dans l'application, avec des détails tels que le nom, la date, les participants, etc.
public class Tournoi
{
    public int id { get; set; }
    public string? nom { get; set; }
    public DateTime dateDebut { get; set; }
    public DateTime dateFin { get; set; }
    public int capacité { get; set; }

    // Un tournoi est associé à un jeu.
    public int jeuId { get; set; }
}
