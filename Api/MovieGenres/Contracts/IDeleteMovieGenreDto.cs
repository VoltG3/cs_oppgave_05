
namespace cs_oppgave_05.Api.MovieGenres.Dtos.Contracts
{
    /// <summary>
    /// Contract for deleting a Movie-Genre link by composite key in request body.
    /// </summary>
    public interface IMovieGenresDeleteDto
    {
        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Genre identifier.</summary>
        int GenId { get; }
    }
}
