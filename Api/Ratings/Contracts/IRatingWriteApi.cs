using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Ratings.Dtos;

namespace cs_oppgave_05.Api.Ratings.Contracts
{
    /// <summary>
    /// Write endpoints for Ratings (POST/PATCH/DELETE).
    /// </summary>
    public interface IRatingWriteApi
    {
        /// <summary>
        /// Creates a new rating.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// POST /api/ratings
        /// Content-Type: application/json
        /// { "movId": 10, "revId": 3, "revStars": 5, "numOfRatings": 1 }
        /// </remarks>
        Task<ActionResult<Rating>> Create([FromBody] CreateRatingDto dto);

        /// <summary>
        /// Partially updates an existing rating by composite key.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// PATCH /api/ratings/{movId}/{revId}
        /// Content-Type: application/json
        /// { "revStars": 4, "numOfRatings": 2 }
        /// </remarks>
        Task<IActionResult> Patch(int movId, int revId, [FromBody] UpdateRatingDto dto);

        /// <summary>
        /// Deletes a rating by composite key in route.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/ratings/{movId}/{revId}
        /// </remarks>
        Task<IActionResult> DeleteById(int movId, int revId);

        /// <summary>
        /// Deletes a rating by composite key in request body.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// DELETE /api/ratings
        /// Content-Type: application/json
        /// { "movId": 10, "revId": 3 }
        /// </remarks>
        Task<IActionResult> DeleteByBody([FromBody] RatingDeleteDto dto);
    }
}
