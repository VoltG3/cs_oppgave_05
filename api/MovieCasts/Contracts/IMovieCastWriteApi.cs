using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Data.DTOs.MovieCast;
using cs_oppgave_05.Data.DTOs.MovieCasts;

namespace cs_oppgave_05.Api.MovieCasts.Contracts
{
    /// <summary>
    /// Write endpoints for MovieCast (POST/PATCH/DELETE).
    /// </summary>
    public interface IMovieCastWriteApi
    {
        /// <summary>
        /// Creates a new movie-cast link.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/movie_casts
        /// Content-Type: application/json
        /// { "actId": 5, "movId": 10, "role": "Neo" }
        /// </remarks>
        Task<ActionResult<MovieCast>> Create([FromBody] CreateMovieCastDto dto);

        /// <summary>
        /// Partially updates a movie-cast link (only role).
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/movie_casts/{actId}/{movId}
        /// Content-Type: application/json
        /// { "role": "Agent Smith" }
        /// </remarks>
        Task<IActionResult> Patch(int actId, int movId, [FromBody] UpdateMovieCastDto dto);

        /// <summary>
        /// Deletes a movie-cast link by composite key in route.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_casts/{actId}/{movId}
        /// </remarks>
        Task<IActionResult> DeleteById(int actId, int movId);

        /// <summary>
        /// Deletes a movie-cast link by composite key in request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_casts
        /// Content-Type: application/json
        /// { "actId": 5, "movId": 10 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] MovieCastDeleteDto dto);
    }
}
