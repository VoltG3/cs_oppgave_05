using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieCasts.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieCasts.Dtos
{
    /// <summary>Request body for partial updates of movie-cast link; nulls are ignored.</summary>
    public class UpdateMovieCastDto : IUpdateMovieCastDto
    {
        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
