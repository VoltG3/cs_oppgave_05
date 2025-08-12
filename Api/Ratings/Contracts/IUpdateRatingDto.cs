
namespace cs_oppgave_05.Api.Ratings.Dtos.Contracts
{
    /// <summary>
    /// Contract for partial rating updates (PATCH /api/ratings/{movId}/{revId}). Nulls mean "no change".
    /// </summary>
    public interface IUpdateRatingDto
    {
        /// <summary>Star rating value (optional).</summary>
        decimal? RevStars { get; }

        /// <summary>Number of ratings (optional).</summary>
        int? NumOfRatings { get; }
    }
}
