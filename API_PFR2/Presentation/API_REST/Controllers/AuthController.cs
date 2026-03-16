using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.Presentation.API_REST.DTO.Requests;
using API_PFR2.Presentation.API_REST.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API_PFR2.Presentation.API_REST.Controllers;
/// <summary>
/// Handles authentication-related HTTP requests.
/// </summary>
/// <remarks>
/// This controller exposes the login endpoint, which validates user credentials
/// and returns a JWT token upon successful authentication.
/// </remarks>

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : APIBaseController
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Authenticates a user and returns a JWT token if successful.
    /// </summary>
    /// <param name="request">The login request containing the user's email and password.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> containing a <see cref="LoginResponse"/> with the JWT token
    /// if authentication succeeded; otherwise, an appropriate error response.
    /// </returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request.Email, request.Password);
        if (token == null)
        {
            return Unauthorized(new { Message = "Invalid email or password." });
        }
        return Ok(new LoginResponse { Token = token });
    }
}
