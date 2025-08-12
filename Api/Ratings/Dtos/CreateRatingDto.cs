using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Ratings.Dtos.Contracts;

namespace cs_oppgave_05.Api.Ratings.Dtos
{
    /// <summary>Request body for creating a rating.</summary>
    public class CreateRatingDto : ICreateRatingDto
    {
        [JsonPropertyName("movId")]
        public int MovId { get; set; }

        [JsonPropertyName("revId")]
        public int RevId { get; set; }

        [JsonPropertyName("revStars")]
        public decimal? RevStars { get; set; }

        [JsonPropertyName("numOfRatings")]
        public int? NumOfRatings { get; set; }
    }
}
