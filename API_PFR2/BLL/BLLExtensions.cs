using Microsoft.Extensions.DependencyInjection;



using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PFR2TestUnitaires")]


namespace API_PFR2.BLL;

/// <summary>
/// 
/// </summary>
public class BLLOptions
{
    // Peut servir à configurer des options spécifiques pour la couche BLL.
    // Ce n'est pas nécessaire pour l'instant, mais cela permet de préparer le terrain pour une future configuration si besoin.

}

/// <summary>
/// 
/// </summary>
public static class BLLExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddBLL(this IServiceCollection services, Action<BLLOptions>? configure = null)
    {
        BLLOptions options = new BLLOptions();
        configure?.Invoke(options); // Permet de configurer les options si une action de configuration est fournie.

        // Ci-dessous on enregistre les services de la couche BLL dans le conteneur de dépendances.
        //services.AddScoped<IJeuService, JeuService>();
        // Enregistre le service métier pour les jeux (IJeuService) avec son implémentation (JeuService) en tant que service à durée de vie Scoped.

        // services.AddScoped<ITournoiService, TournoiService>();
        // services.AddScoped<IReservationService, ReservationService>();
        // services.AddScoped<IUtilisateurService, UtilisateurService>();

        return services;
    }

}
