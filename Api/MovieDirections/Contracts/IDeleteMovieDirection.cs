
namespace cs_oppgave_05.Api.MovieDirections.Dtos.Contracts
{
    /// <summary>
    /// Contract for deleting a movie-direction link by composite key in request body.
    /// </summary>
    public interface IMovieDirectionDeleteDto
    {
        /// <summary>Director identifier.</summary>
        int DirId { get; }

        /// <summary>Movie identifier.</summary>
        int MovId { get; }
    }
}
