using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieGenres.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieGenres.Dtos
{
    /// <summary>Request body for creating a Movie-Genre link.</summary>
    public class CreateMovieGenresDto : ICreateMovieGenresDto
    {
        [JsonPropertyName("movId")]
        public int MovId { get; set; }

        [JsonPropertyName("genId")]
        public int GenId { get; set; }
    }
}
