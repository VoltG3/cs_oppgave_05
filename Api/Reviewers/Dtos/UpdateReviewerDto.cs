using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Reviewers.Dtos.Contracts;

namespace cs_oppgave_05.Api.Reviewers.Dtos
{
    /// <summary>Request body for partial reviewer updates; nulls are ignored.</summary>
    public class UpdateReviewerDto : IUpdateReviewerDto
    {
        [JsonPropertyName("revName")]
        public string? RevName { get; set; }
    }
}
