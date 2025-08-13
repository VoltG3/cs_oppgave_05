using Microsoft.AspNetCore.Mvc;

namespace cs_oppgave_05.Api.Genres.Contracts
{
    /// <summary>
    /// Read-only endpoints for Genres (GET operations).
    /// </summary>
    public interface IGenreReadApi
    {
        /// <summary>
        /// Returns all genres.
        /// </summary>
        /// <remarks>
        /// REQUEST (browser-friendly):
        /// GET /api/genres
        /// </remarks>
        Task<ActionResult<IEnumerable<Entities.Genres>>> GetAll();

        /// <summary>
        /// Returns a single genre by its ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/genres/{id}
        /// Example: GET /api/genres/7
        /// </remarks>
        Task<ActionResult<Entities.Genres>> GetById(int id);
    }
}
