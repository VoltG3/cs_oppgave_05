using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Directors.Dtos.Contracts;

namespace cs_oppgave_05.Api.Directors.Dtos
{
    /// <summary>Request body for partial director updates; nulls are ignored.</summary>
    public class UpdateDirectorDto : IUpdateDirectorDto
    {
        [JsonPropertyName("dirFname")]
        public string? DirFname { get; set; }

        [JsonPropertyName("dirLname")]
        public string? DirLname { get; set; }
    }
}
