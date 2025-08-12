using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieDirections.Dtos
{
    /// <summary>
    /// Empty update DTO: MovieDirection has no mutable fields.
    /// Controllers should return 400 for PATCH.
    /// </summary>
    public class UpdateMovieDirectionDto : IUpdateMovieDirectionDto
    {
        // intentionally empty
    }
}
