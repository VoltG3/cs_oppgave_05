
namespace cs_oppgave_05.Api.Reviewers.Dtos.Contracts
{
    /// <summary>
    /// Contract for partial reviewer updates (PATCH /api/reviewers/{id}). Nulls mean "no change".
    /// </summary>
    public interface IUpdateReviewerDto
    {
        /// <summary>Reviewer name (optional).</summary>
        string? RevName { get; }
    }
}
