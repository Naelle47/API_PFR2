namespace API_PFR2.Presentation.API_REST.DTO.Responses
{
    /// <summary>
    /// Represents the response returned after a successful authentication.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the JWT token generated upon successful authentication.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
