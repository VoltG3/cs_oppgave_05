
namespace cs_oppgave_05.Api.MovieGenres.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a Movie-Genre link (POST /api/movie_genres).
    /// </summary>
    public interface ICreateMovieGenresDto
    {
        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Genre identifier.</summary>
        int GenId { get; }
    }
}
