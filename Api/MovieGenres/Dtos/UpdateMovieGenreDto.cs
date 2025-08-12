using cs_oppgave_05.Api.MovieGenres.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieGenres.Dtos
{
    /// <summary>
    /// Empty update DTO: MovieGenres has no mutable fields.
    /// Controllers should return 400 for PATCH.
    /// </summary>
    public class UpdateMovieGenresDto : IUpdateMovieGenresDto
    {
        // intentionally empty
    }
}
