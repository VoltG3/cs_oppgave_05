
namespace cs_oppgave_05.Api.MovieCasts.Dtos.Contracts
{
    /// <summary>
    /// Contract for partially updating a movie-cast link (PATCH /api/movie_casts/{actId}/{movId}).
    /// Nulls mean "no change".
    /// </summary>
    public interface IUpdateMovieCastDto
    {
        /// <summary>Character/role name (optional).</summary>
        string? Role { get; }
    }
}
