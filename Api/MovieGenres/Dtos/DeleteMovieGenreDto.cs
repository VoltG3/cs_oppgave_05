using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieGenres.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieGenres.Dtos
{
    /// <summary>Request body for deleting a Movie-Genre link by composite key.</summary>
    public sealed class MovieGenresDeleteDto : IMovieGenresDeleteDto
    {
        [JsonPropertyName("movId")]
        public int MovId { get; set; }

        [JsonPropertyName("genId")]
        public int GenId { get; set; }
    }
}
