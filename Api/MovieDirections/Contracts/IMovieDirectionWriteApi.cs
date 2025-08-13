using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.MovieDirections.Dtos;
using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieDirections.Contracts
{
    /// <summary>
    /// Write endpoints for MovieDirection (POST/PATCH/DELETE).
    /// </summary>
    public interface IMovieDirectionWriteApi
    {
        /// <summary>
        /// Creates a new movie-direction link.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/movie_directions
        /// Content-Type: application/json
        /// { "dirId": 5, "movId": 10 }
        /// </remarks>
        Task<ActionResult<MovieDirection>> Create([FromBody] CreateMovieDirectionDto dto);

        /// <summary>
        /// Not supported: key-only link; keys cannot be changed.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/movie_directions/{dirId}/{movId}
        /// Response: 400 Bad Request
        /// </remarks>
        Task<IActionResult> Patch(int dirId, int movId);

        /// <summary>
        /// Deletes a movie-direction link by composite key in route.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_directions/{dirId}/{movId}
        /// </remarks>
        Task<IActionResult> DeleteById(int dirId, int movId);

        /// <summary>
        /// Deletes a movie-direction link by composite key in request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_directions
        /// Content-Type: application/json
        /// { "dirId": 5, "movId": 10 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] MovieDirectionDeleteDto dto);
    }
}
