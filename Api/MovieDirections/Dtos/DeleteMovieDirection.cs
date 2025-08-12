using System.Text.Json.Serialization;
using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts;

namespace cs_oppgave_05.Api.MovieDirections.Dtos.Contracts
{
    /// <summary>Request body for deleting a movie-direction link by composite key.</summary>
    public sealed class MovieDirectionDeleteDto : IMovieDirectionDeleteDto
    {
        [JsonPropertyName("dirId")]
        public int DirId { get; set; }   // from original DTO

        [JsonPropertyName("movId")]
        public int MovId { get; set; }   // from original DTO
    }
}
