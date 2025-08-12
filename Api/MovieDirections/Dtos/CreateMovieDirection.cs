using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieDirections.Dtos
{
    /// <summary>Request body for creating a movie-direction link.</summary>
    public class CreateMovieDirectionDto : ICreateMovieDirectionDto
    {
        [JsonPropertyName("dirId")]
        public int DirId { get; set; }   // from original DTO

        [JsonPropertyName("movId")]
        public int MovId { get; set; }   // from original DTO
    }
}
