using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Actors.Dtos.Contracts;

namespace cs_oppgave_05.Api.Actors.Dtos
{
    /// <summary>Request body for creating an actor.</summary>
    public class CreateActorDto : ICreateActorDto
    {
        [JsonPropertyName("actFname")]
        public string ActFname { get; set; } = string.Empty;

        [JsonPropertyName("actLname")]
        public string ActLname { get; set; } = string.Empty;

        [JsonPropertyName("actGender")]
        public string ActGender { get; set; } = string.Empty; // "M" or "F"
    }
}
