using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Genres.Dtos.Contracts;

namespace cs_oppgave_05.Api.Genres.Dtos
{
    /// <summary>Request body for partial genre updates; nulls are ignored.</summary>
    public class UpdateGenreDto : IUpdateGenreDto
    {
        [JsonPropertyName("genTitle")]
        public string? GenTitle { get; set; }
    }
}
