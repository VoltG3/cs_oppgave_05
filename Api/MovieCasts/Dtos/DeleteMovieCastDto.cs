using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieCasts.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieCasts.Dtos
{
    /// <summary>Request body for deleting a movie-cast link by composite key.</summary>
    public class MovieCastDeleteDto : IMovieCastDeleteDto
    {
        [JsonPropertyName("actId")]
        public int ActId { get; set; }

        [JsonPropertyName("movId")]
        public int MovId { get; set; }
    }
}
