using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using FluentValidation.Results;


namespace API_PFR2.Presentation.API_REST.Controllers;
/// <summary>
/// Cette classe abstraite sert de base pour tous les contrôleurs d'API REST dans l'application.
/// </summary>

[Authorize(Roles = "Utilisateur")]
[ApiController]
[Route("api/[controller]")]
public abstract class APIBaseController : ControllerBase
{
    /// <summary>
    /// Validates the specified request using the provided validator and returns a BadRequestObjectResult containing
    /// validation error details if the request is invalid.
    /// </summary>
    /// <remarks>This method performs asynchronous validation and returns a structured response for validation
    /// errors, including a problem details object that conforms to RFC 7231. The returned BadRequestObjectResult
    /// includes details about each validation failure, making it suitable for use in API responses.</remarks>
    /// <typeparam name="R">The type of the request object to validate.</typeparam>
    /// <typeparam name="V">The type of the validator used to validate the request object. Must inherit from <see cref="AbstractValidator{R}"/>.</typeparam>
    /// <param name="request">The request object to be validated.</param>
    /// <param name="validator">The validator instance that performs validation on the request object.</param>
    /// <returns>A BadRequestObjectResult containing validation error details if the request is invalid; otherwise, null.</returns>
    protected async Task<BadRequestObjectResult?> ValidateRequest<R, V>(R request, V validator)
        where V : AbstractValidator<R>
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation error",
                Status = StatusCodes.Status400BadRequest
            };

            return BadRequest(problemDetails);
        }

        return null;
    }
}
