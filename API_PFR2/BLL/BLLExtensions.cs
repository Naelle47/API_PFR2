using API_PFR2.BLL.Services.Implementations;
using API_PFR2.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;



using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PFR2TestUnitaires")]


namespace API_PFR2.BLL;

/// <summary>
/// Represents configuration options for the Business Logic Layer.
/// </summary>
/// <remarks>
/// This class can be used to configure specific behaviors or settings
/// for services registered in the BLL. It currently serves as a placeholder
/// for future configuration needs.
/// </remarks>
public class BLLOptions
{
    // Peut servir à configurer des options spécifiques pour la couche BLL.
    // Ce n'est pas nécessaire pour l'instant, mais cela permet de préparer le terrain pour une future configuration si besoin.

}

/// <summary>
/// Provides extension methods for registering Business Logic Layer services
/// in the dependency injection container.
/// </summary>
/// <remarks>
/// This class centralizes the registration of application services
/// that contain business logic.
/// </remarks>
public static class BLLExtensions
{
    /// <summary>
    /// Registers the Business Logic Layer services in the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The service collection used to register application services.
    /// </param>
    /// <param name="configure">
    /// Optional configuration action used to customize <see cref="BLLOptions"/>.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with BLL services registered.
    /// </returns>
    public static IServiceCollection AddBLL(this IServiceCollection services, Action<BLLOptions>? configure = null)
    {
        BLLOptions options = new BLLOptions();
        configure?.Invoke(options); // Permet de configurer les options si une action de configuration est fournie.

        // Ci-dessous on enregistre les services de la couche BLL dans le conteneur de dépendances.
        // Utilisation de AddScoped pour les services qui doivent être partagés au sein d'une même requête HTTP, mais pas entre différentes requêtes.
        services.AddScoped<IJeuService, JeuService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITournoiService, TournoiService>();
        services.AddScoped<IInscriptionTournoiService, InscriptionTournoiService>();

        //services.AddScoped<IUtilisateurService, UtilisateurService>();

        // Utilisation de AddTransient pour les services qui n'ont pas besoin d'être partagés.
        services.AddTransient<IEmailService, EmailService>(); 
       
        return services;
    }

}
