
using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Ratings.Dtos.Contracts;

namespace cs_oppgave_05.Api.Ratings.Dtos
{
    /// <summary>Request body for partial rating updates; nulls are ignored.</summary>
    public class UpdateRatingDto : IUpdateRatingDto
    {
        [JsonPropertyName("revStars")]
        public decimal? RevStars { get; set; }

        [JsonPropertyName("numOfRatings")]
        public int? NumOfRatings { get; set; }
    }
}
