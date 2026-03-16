using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_PFR2.BLL.Services.Interfaces;
using API_PFR2.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace API_PFR2.BLL.Services.Implementations
{
    /// <summary>
    /// Provides authentication-related business logic, including user login and JWT token generation.
    /// </summary>
    /// <remarks>
    /// This service verifies user credentials against the database and generates a signed JWT token
    /// upon successful authentication. Password verification is handled using BCrypt.
    /// </remarks>
    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUtilisateurRepository utlisateurRepository, IConfiguration configuration)
        { 
            _utilisateurRepository = utlisateurRepository;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            // Retrieve the user from the database using their email address

            var utilisateur = await _utilisateurRepository.GetByEmailAsync(email);

            // If the user does not exist or the password is incorrect, return null
            if(utilisateur == null) 
               return null;

            // Verify the provided password against the stored password hash
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, utilisateur.passwordHash);
            if (!isPasswordValid)
                return null;

            // If authentication is successful, generate a JWT token for the user
            return GenerateJwtToken(utilisateur);
        }

        private string GenerateJwtToken(API_PFR2.Domain.Entities.Utilisateur utilisateur)
        {
            var secret = _configuration["JWTSecret"]!;
            var issuer = _configuration["JWTIssuer"]!;
            var audience = _configuration["JWTAudience"]!;
            var expireInSeconds = int.Parse(_configuration["JWTExpireTokenInSeconds"] ?? "3600");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.id.ToString()),
                new Claim(ClaimTypes.Email, utilisateur.email),
                new Claim(ClaimTypes.Role, utilisateur.role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(expireInSeconds),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
