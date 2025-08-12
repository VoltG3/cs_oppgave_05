using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Actors.Dtos.Contracts;

namespace cs_oppgave_05.Api.Actors.Dtos
{
    /// <summary>Request body for partial actor updates; nulls are ignored.</summary>
    public class UpdateActorDto : IUpdateActorDto
    {
        [JsonPropertyName("actFname")]
        public string? ActFname { get; set; }

        [JsonPropertyName("actLname")]
        public string? ActLname { get; set; }

        [JsonPropertyName("actGender")]
        public string? ActGender { get; set; }
    }
}
