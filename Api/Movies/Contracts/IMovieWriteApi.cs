using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Movies.Contracts
{
    /// <summary>
    /// Write endpoints for Movies (POST/PATCH/DELETE).
    /// </summary>
    public interface IMovieWriteApi
    {
        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/movies
        /// Content-Type: application/json
        /// {
        ///   "movTitle": "Inception",
        ///   "movYear": 2010,
        ///   "movTime": 148,
        ///   "movLang": "English",
        ///   "movDtRel": "2010-07-16",
        ///   "movRelCountry": "USA"
        /// }
        /// </remarks>
        Task<ActionResult<Movie>> Create([FromBody] CreateMovieDto dto);

        /// <summary>
        /// Partially updates a movie by ID using a typed DTO (nulls are ignored).
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/movies/{id}
        /// Content-Type: application/json
        /// { "movTitle": "New Title", "movYear": 1999 }
        /// </remarks>
        Task<IActionResult> Patch(int id, [FromBody] UpdateMovieDto dto);

        /// <summary>
        /// Deletes a movie by route ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movies/{id}
        /// Example: DELETE /api/movies/42
        /// </remarks>
        Task<IActionResult> DeleteById(int id);

        /// <summary>
        /// Deletes a movie by passing an ID DTO in the request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/movies
        /// Content-Type: application/json
        /// { "id": 42 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] cs_oppgave_05.Api._Shared.Dtos.DeleteByIdDto dto);
    }
}
