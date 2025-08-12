
namespace cs_oppgave_05.Api.MovieCasts.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a movie-cast link (POST /api/movie_casts).
    /// </summary>
    public interface ICreateMovieCastDto
    {
        /// <summary>Actor identifier.</summary>
        int ActId { get; }

        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Character/role name.</summary>
        string Role { get; }
    }
}
