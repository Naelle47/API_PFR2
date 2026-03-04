namespace PFR2_API.Domain.Enums;

// Enumération représentant les différents statuts possibles pour une inscription à un tournoi.
// Les statuts peuvent inclure : "En attente", "Validée", "Annulée", etc. selon les règles de gestion de l'application.

public enum StatutInscription
{
    EnAttente = 0, // L'inscription est en attente de confirmation ou de validation. Sera la valeur par défaut lors de la création d'une nouvelle inscription.
    Validee = 1, // L'inscription a été validée et confirmée pour le tournoi.
    Refusee = 2, // L'inscription a été refusée, soit par l'administrateur, soit en raison de critères non remplis.
}
