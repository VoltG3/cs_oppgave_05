using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api._Shared.Dtos;
using cs_oppgave_05.Api.Actors.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Actors.Contracts
{
    /// <summary>
    /// Write endpoints for Actors (POST/PATCH/DELETE).
    /// </summary>
    public interface IActorWriteApi
    {
        /// <summary>
        /// Creates a new actor.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/actors
        /// Content-Type: application/json
        /// {
        ///   "actFname": "Keanu",
        ///   "actLname": "Reeves",
        ///   "actGender": "M"
        /// }
        /// </remarks>
        Task<ActionResult<Actor>> Create([FromBody] CreateActorDto dto);

        /// <summary>
        /// Partially updates an actor by ID; null properties are ignored.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/actors/{id}
        /// Content-Type: application/json
        /// { "actFname": "John", "actGender": "M" }
        /// </remarks>
        Task<IActionResult> Patch(int id, [FromBody] UpdateActorDto dto);

        /// <summary>
        /// Deletes an actor by route ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/actors/{id}
        /// Example: DELETE /api/actors/42
        /// </remarks>
        Task<IActionResult> DeleteById(int id);

        /// <summary>
        /// Deletes an actor by passing an ID DTO in the request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/actors
        /// Content-Type: application/json
        /// { "id": 42 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto);
    }
}
