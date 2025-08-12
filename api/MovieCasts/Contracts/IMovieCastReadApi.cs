using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Api.MovieCasts.Contracts
{
    /// <summary>
    /// Read-only endpoints for MovieCast (GET operations).
    /// </summary>
    public interface IMovieCastReadApi
    {
        /// <summary>
        /// Returns movie-cast links. If both actId and movId are provided, returns a single link.
        /// </summary>
        /// <remarks>
        /// REQUESTS:
        /// GET /api/movie_casts
        /// GET /api/movie_casts?actId=5&movId=10
        /// </remarks>
        Task<ActionResult<IEnumerable<MovieCast>>> GetAll(int? actId, int? movId);

        /// <summary>
        /// Returns a single movie-cast link by composite key.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/movie_casts/{actId}/{movId}
        /// Example: GET /api/movie_casts/5/10
        /// </remarks>
        Task<ActionResult<MovieCast>> GetById(int actId, int movId);
    }
}
