using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Movies.Dtos.Contracts;

namespace cs_oppgave_05.Api.Movies.Dtos
{
    public class CreateMovieDto : ICreateMovieDto
    {
        [JsonPropertyName("movTitle")]
        public string MovTitle { get; set; } = string.Empty;

        [JsonPropertyName("movYear")]
        public int? MovYear { get; set; }

        [JsonPropertyName("movTime")]
        public int? MovTime { get; set; }

        [JsonPropertyName("movLang")]
        public string? MovLang { get; set; }

        [JsonPropertyName("movDtRel")]
        public DateTime? MovDtRel { get; set; }

        [JsonPropertyName("movRelCountry")]
        public string? MovRelCountry { get; set; }
    }
}
