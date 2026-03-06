using API_PFR2.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;




namespace API_PFR2.Presentation.API_REST.Filters;

/// <summary>
/// Provides a custom exception filter for ASP.NET Core API controllers that enables tailored error responses based on
/// the type of exception encountered during action execution.
/// </summary>
/// <remarks>This attribute allows registration of specific exception handlers for known exception types, such as
/// NotFoundEntityException, UnauthorizedAccessException, and others. It ensures that API clients receive appropriate
/// HTTP status codes and detailed error information for common error scenarios, including invalid model states and
/// unknown exceptions. The filter can be extended to handle additional exception types as needed, promoting consistent
/// and informative error handling across the API.</remarks>
public class APIExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    /// <summary>
    /// Initializes a new instance of the APIExceptionFilterAttribute class, configuring handlers for known exception types
    /// to enable custom error responses in API controllers.
    /// </summary>
    /// <remarks>This constructor sets up a mapping between exception types and their corresponding handling actions.
    /// By default, it registers a handler for NotFoundEntityException, allowing the filter to generate appropriate API
    /// responses when such exceptions are encountered. Additional exception handlers can be added to extend error handling
    /// as needed.</remarks>
    public APIExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            {typeof (NotFoundEntityException),  HandleNotFoundException},
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException }
        };
    }

    /// <summary>
    /// Handles exceptions that occur during the execution of an action method in an ASP.NET Core application.
    /// </summary>
    /// <remarks>This method enables custom exception handling logic to be executed before invoking the base
    /// exception handling. Override this method to implement application-specific error handling or logging
    /// strategies.</remarks>
    /// <param name="context">The context for the exception, which provides information about the exception and the current HTTP request.</param>
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    /// <summary>
    /// Determines the appropriate handler for the current exception and executes it.
    /// </summary>
    /// <param name="context">The exception context containing details about the current request and exception.</param>
    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {

            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }


    /// <summary>
    /// Handles invalid model state errors and returns a 400 Bad Request response.
    /// </summary>
    /// <param name="context">The exception context associated with the current request.</param>
    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles <see cref="NotFoundEntityException"/> and returns a 404 Not Found response.
    /// </summary>
    /// <param name="context">The exception context associated with the current request.</param>
    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundEntityException;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception?.Message,

        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles unauthorized access exceptions and returns a 401 Unauthorized response.
    /// </summary>
    /// <param name="context">The exception context associated with the current request.</param>
    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handles forbidden access exceptions and returns a 403 Forbidden response.
    /// </summary>
    /// <param name="context">The exception context associated with the current request.</param>
    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }


    /// <summary>
    /// Handles unexpected exceptions and returns a 500 Internal Server Error response.
    /// </summary>
    /// <param name="context">The exception context associated with the current request.</param>
    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }



}
