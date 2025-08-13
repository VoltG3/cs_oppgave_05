using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.MovieDirections.Contracts
{
    /// <summary>
    /// Read-only endpoints for MovieDirection (GET operations).
    /// </summary>
    public interface IMovieDirectionReadApi
    {
        /// <summary>
        /// Returns movie-direction links. If both dirId and movId are supplied, returns a single link.
        /// </summary>
        /// <remarks>
        /// REQUESTS:
        /// GET /api/movie_directions
        /// GET /api/movie_directions?dirId=5&movId=10
        /// </remarks>
        Task<ActionResult<IEnumerable<MovieDirection>>> GetAll(int? dirId, int? movId);

        /// <summary>
        /// Returns a single movie-direction link by composite key.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/movie_directions/{dirId}/{movId}
        /// Example: GET /api/movie_directions/5/10
        /// </remarks>
        Task<ActionResult<MovieDirection>> GetById(int dirId, int movId);
    }
}
