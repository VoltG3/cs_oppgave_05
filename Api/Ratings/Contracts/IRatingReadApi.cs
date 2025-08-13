using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Ratings.Contracts
{
    /// <summary>
    /// Read-only endpoints for Ratings (GET operations).
    /// </summary>
    public interface IRatingReadApi
    {
        /// <summary>
        /// Returns ratings. If both movId and revId are supplied, returns a single rating.
        /// </summary>
        /// <remarks>
        /// REQUESTS:
        /// GET /api/ratings
        /// GET /api/ratings?movId=10&revId=3
        /// </remarks>
        Task<ActionResult<IEnumerable<Rating>>> GetAll(int? movId, int? revId);

        /// <summary>
        /// Returns a single rating by composite key.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/ratings/{movId}/{revId}
        /// Example: GET /api/ratings/10/3
        /// </remarks>
        Task<ActionResult<Rating>> GetById(int movId, int revId);
    }
}
