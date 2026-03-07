namespace API_PFR2.Presentation.API_REST.DTO.Requests;

/// <summary>
/// Represents the parameters used to query reservations.
/// </summary>
/// <remarks>
/// This DTO can be used to filter reservations based on the game identifier and a specific date.
/// Typically, it is used for filtering reservations when retrieving them from the system, allowing clients to specify criteria for the search.
/// </remarks>
public class ReservationQuery
{
    /// <summary>
    /// Gets or sets the identifier of the game.
    /// </summary>
    public int JeuId { get; set; }

    /// <summary>
    /// Gets or sets the date used to filter reservations.
    /// </summary>
    public DateTime Date { get; set; }
}
