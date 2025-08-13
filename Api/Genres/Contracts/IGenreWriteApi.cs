using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api._Shared.Dtos;
using cs_oppgave_05.Api.Genres.Dtos;

namespace cs_oppgave_05.Api.Genres.Contracts
{
    /// <summary>
    /// Write endpoints for Genres (POST/PATCH/DELETE).
    /// </summary>
    public interface IGenreWriteApi
    {
        /// <summary>
        /// Creates a new genre.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/genres
        /// Content-Type: application/json
        /// { "genTitle": "Sci-Fi" }
        /// </remarks>
        Task<ActionResult<Entities.Genres>> Create([FromBody] CreateGenreDto dto);

        /// <summary>
        /// Partially updates a genre by ID; null properties are ignored.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/genres/{id}
        /// Content-Type: application/json
        /// { "genTitle": "Science Fiction" }
        /// </remarks>
        Task<IActionResult> Patch(int id, [FromBody] UpdateGenreDto dto);

        /// <summary>
        /// Deletes a genre by route ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/genres/{id}
        /// Example: DELETE /api/genres/7
        /// </remarks>
        Task<IActionResult> DeleteById(int id);

        /// <summary>
        /// Deletes a genre by passing an ID DTO in the request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/genres
        /// Content-Type: application/json
        /// { "id": 7 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto);
    }
}
