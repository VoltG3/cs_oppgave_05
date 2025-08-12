
namespace cs_oppgave_05.Api.Ratings.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a rating (POST /api/ratings).
    /// </summary>
    public interface ICreateRatingDto
    {
        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Reviewer identifier.</summary>
        int RevId { get; }

        /// <summary>Star rating value (optional).</summary>
        decimal? RevStars { get; }

        /// <summary>Number of ratings (optional).</summary>
        int? NumOfRatings { get; }
    }
}
