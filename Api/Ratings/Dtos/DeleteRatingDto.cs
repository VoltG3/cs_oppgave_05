using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Ratings.Dtos.Contracts;

namespace cs_oppgave_05.Api.Ratings.Dtos
{
    /// <summary>Request body for deleting a rating by composite key.</summary>
    public sealed class RatingDeleteDto : IRatingDeleteDto
    {
        [JsonPropertyName("movId")]
        public int MovId { get; set; }

        [JsonPropertyName("revId")]
        public int RevId { get; set; }
    }
}
