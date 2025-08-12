
using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Genres.Dtos.Contracts;

namespace cs_oppgave_05.Api.Genres.Dtos
{
    /// <summary>Request body for creating a genre.</summary>
    public class CreateGenreDto : ICreateGenreDto
    {
        [JsonPropertyName("genTitle")]
        public string GenTitle { get; set; } = string.Empty;
    }
}
