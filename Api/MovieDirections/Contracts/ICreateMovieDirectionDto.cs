
namespace cs_oppgave_05.Api.MovieDirections.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a movie-direction link (POST /api/movie_directions).
    /// </summary>
    public interface ICreateMovieDirectionDto
    {
        /// <summary>Director identifier.</summary>
        int DirId { get; }

        /// <summary>Movie identifier.</summary>
        int MovId { get; }
    }
}
