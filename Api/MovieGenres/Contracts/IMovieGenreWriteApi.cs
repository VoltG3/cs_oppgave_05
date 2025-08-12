using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Dtos;

namespace cs_oppgave_05.Api.MovieGenres.Contracts
{
    /// <summary>
    /// Write endpoints for MovieGenres (POST/PATCH/DELETE).
    /// </summary>
    public interface IMovieGenresWriteApi
    {
        /// <summary>
        /// Creates a new movie-genre link.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/movie_genres
        /// Content-Type: application/json
        /// { "movId": 10, "genId": 3 }
        /// </remarks>
        Task<ActionResult<Models.MovieGenres>> Create([FromBody] CreateMovieGenresDto dto);

        /// <summary>
        /// Not supported: movie_genres is key-only link; keys cannot be changed.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/movie_genres/{movId}/{genId}
        /// Response: 400 Bad Request
        /// </remarks>
        Task<IActionResult> Patch(int movId, int genId);

        /// <summary>
        /// Deletes a movie-genre link by composite key in route.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_genres/{movId}/{genId}
        /// </remarks>
        Task<IActionResult> DeleteById(int movId, int genId);

        /// <summary>
        /// Deletes a movie-genre link by composite key in request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movie_genres
        /// Content-Type: application/json
        /// { "movId": 10, "genId": 3 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] MovieGenresDeleteDto dto);
    }
}
