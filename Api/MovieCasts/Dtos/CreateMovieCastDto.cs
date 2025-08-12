using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieCasts.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieCasts.Dtos
{
    /// <summary>Request body for creating a movie-cast link.</summary>
    public class CreateMovieCastDto : ICreateMovieCastDto
    {
        [JsonPropertyName("actId")]
        public int ActId { get; set; }

        [JsonPropertyName("movId")]
        public int MovId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
    }
}
