namespace PFR2_API.Domain.Entities;

// Classe représentant une réservation d’un jeu par un utilisateur sur une plage horaire.

public class Reservation
{
    public int id { get; set; }
    public DateTime dateDebut { get; set; }
    public DateTime dateFin { get; set; }

    // Une réservation est associée à un utilisateur et un jeu.
    public int utilisateurId { get; set; }
    public int jeuId { get; set; }

    // Propriétés de navigation pour les relations avec les autres entités.
    public required Utilisateur Utilisateur { get; set; }
    public required Jeu Jeu { get; set; }
}
