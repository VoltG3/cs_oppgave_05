
namespace cs_oppgave_05.Api.MovieGenres.Dtos.Contracts
{
    /// <summary>
    /// Marker contract for updates. MovieGenres has no mutable fields;
    /// controllers should return 400 for PATCH.
    /// </summary>
    public interface IUpdateMovieGenresDto { }
}
