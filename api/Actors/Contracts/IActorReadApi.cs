using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Api.Actors.Contracts
{
    /// <summary>
    /// Read-only endpoints for Actors (GET operations).
    /// </summary>
    public interface IActorReadApi
    {
        /// <summary>
        /// Returns all actors.
        /// </summary>
        /// <remarks>
        /// REQUEST (browser-friendly):
        /// GET /api/actors
        /// </remarks>
        Task<ActionResult<IEnumerable<Actor>>> GetAll();

        /// <summary>
        /// Returns a single actor by its ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/actors/{id}
        /// Example: GET /api/actors/42
        /// </remarks>
        Task<ActionResult<Actor>> GetById(int id);
    }
}
