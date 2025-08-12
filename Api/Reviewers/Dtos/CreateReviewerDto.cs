using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Reviewers.Dtos.Contracts;

namespace cs_oppgave_05.Api.Reviewers.Dtos
{
    /// <summary>Request body for creating a reviewer.</summary>
    public class CreateReviewerDto : ICreateReviewerDto
    {
        [JsonPropertyName("revName")]
        public string RevName { get; set; } = string.Empty;
    }
}
