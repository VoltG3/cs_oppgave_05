
namespace cs_oppgave_05.Api.Reviewers.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a reviewer (request body for POST /api/reviewers).
    /// </summary>
    public interface ICreateReviewerDto
    {
        /// <summary>Reviewer name.</summary>
        string RevName { get; }
    }
}
