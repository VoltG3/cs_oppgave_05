
namespace cs_oppgave_05.Api.MovieCasts.Dtos.Contracts
{
    /// <summary>
    /// Contract for deleting a movie-cast link by composite key in request body (DELETE /api/movie_casts).
    /// </summary>
    public interface IMovieCastDeleteDto
    {
        /// <summary>Actor identifier.</summary>
        int ActId { get; }

        /// <summary>Movie identifier.</summary>
        int MovId { get; }
    }
}
