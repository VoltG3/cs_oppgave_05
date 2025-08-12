using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Api.Reviewers.Contracts
{
    /// <summary>
    /// Read-only endpoints for Reviewers (GET operations).
    /// </summary>
    public interface IReviewerReadApi
    {
        /// <summary>
        /// Returns all reviewers.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/reviewers
        /// </remarks>
        Task<ActionResult<IEnumerable<Reviewer>>> GetAll();

        /// <summary>
        /// Returns a single reviewer by ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/reviewers/{id}
        /// Example: GET /api/reviewers/5
        /// </remarks>
        Task<ActionResult<Reviewer>> GetById(int id);
    }
}
