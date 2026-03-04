namespace PFR2_API.Domain.Entities;

// Classe représentant un jeu disponible à la réservation ou utilisé dans un tournoi.
// Les proprétés inclusent sont minimales pour identifier le jeu, son nom et une description.

public class Jeu
{
    public int id { get; set; }
    public string nom { get; set; }
    public string? description { get; set; }
}
