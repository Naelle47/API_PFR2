namespace API_PFR2.Presentation.API_REST.DTO.Requests
{
    /// <summary>
    /// Represents the data required to authenticate a user.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the password provided by the user.
        /// </summary>
        public required string Password { get; set; }
    }
}
