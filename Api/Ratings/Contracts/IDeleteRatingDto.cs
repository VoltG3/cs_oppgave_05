
namespace cs_oppgave_05.Api.Ratings.Dtos.Contracts
{
    /// <summary>
    /// Contract for deleting a rating by composite key in request body (DELETE /api/ratings).
    /// </summary>
    public interface IRatingDeleteDto
    {
        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Reviewer identifier.</summary>
        int RevId { get; }
    }
}
