namespace API_PFR2.Domain.Enums;

// Enumération représentant les différents statuts possibles pour une inscription à un tournoi.
// Les statuts peuvent inclure : "En attente", "Validée", "Annulée", etc. selon les règles de gestion de l'application.

/// <summary>
/// Specifies the possible statuses for a tournament registration, such as pending, validated, or refused.
/// </summary>
/// <remarks>Use this enumeration to track and manage the state of a registration throughout the tournament
/// workflow. The status determines which actions are available to participants and administrators at each stage of the
/// registration process.</remarks>
public enum StatutInscription
{
    /// <summary>
    /// Represents the state in which an operation or entity is pending and has not yet been processed or finalized.
    /// </summary>
    EnAttente = 0, // L'inscription est en attente de confirmation ou de validation. Sera la valeur par défaut lors de la création d'une nouvelle inscription.
    /// <summary>
    /// Indicates that the entity is in a valid state.
    /// </summary>
    Validee = 1, // L'inscription a été validée et confirmée pour le tournoi.
    /// <summary>
    /// Represents an applicant whose registration has been refused.
    /// </summary>
    Refusee = 2, // L'inscription a été refusée, soit par l'administrateur, soit en raison de critères non remplis.
}
