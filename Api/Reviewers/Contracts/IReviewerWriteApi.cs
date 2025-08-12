using cs_oppgave_05.Api.Reviewers.Dtos;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using DeleteByIdDto = cs_oppgave_05.Api._Shared.Dtos.DeleteByIdDto;

namespace cs_oppgave_05.Api.Reviewers.Contracts
{
    /// <summary>
    /// Write endpoints for Reviewers (POST/PATCH/DELETE).
    /// </summary>
    public interface IReviewerWriteApi
    {
        /// <summary>
        /// Creates a new reviewer.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/reviewers
        /// Content-Type: application/json
        /// { "revName": "Roger Ebert" }
        /// </remarks>
        Task<ActionResult<Reviewer>> Create([FromBody] CreateReviewerDto dto);

        /// <summary>
        /// Partially updates a reviewer by ID; null properties are ignored.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/reviewers/{id}
        /// Content-Type: application/json
        /// { "revName": "R. Ebert" }
        /// </remarks>
        Task<IActionResult> Patch(int id, [FromBody] UpdateReviewerDto dto);

        /// <summary>
        /// Deletes a reviewer by route ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/reviewers/{id}
        /// </remarks>
        Task<IActionResult> DeleteById(int id);

        /// <summary>
        /// Deletes a reviewer by passing an ID DTO in the request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/reviewers
        /// Content-Type: application/json
        /// { "id": 5 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto);
    }
}
