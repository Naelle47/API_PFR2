namespace API_PFR2.Presentation.API_REST.DTO.Responses;

/// <summary>
/// DTO utilisé pour exposer les jeux à l'extérieur de l'API.
/// On n'expose que les données utiles pour le client, et on masque les données sensibles ou inutiles (ex: description du jeu).
/// On a donc un DTO de réponse qui ne contient que l'ID et le nom du jeu.
/// </summary>
public class JeuResponse
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Nom { get; set; }
}
