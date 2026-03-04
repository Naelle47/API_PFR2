using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using FluentValidation.Results;


namespace API_PFR2.Presentation.API_REST.Controllers;
/// <summary>
/// Cette classe abstraite sert de base pour tous les contrôleurs d'API REST dans l'application.
/// </summary>

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public abstract class APIBaseController : ControllerBase
{
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
