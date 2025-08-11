
namespace cs_oppgave_05.Data.DTOs.Actors
{
    public class CreateActorDto
    {
        public string ActFname { get; set; } = string.Empty;
        public string ActLname { get; set; } = string.Empty;
        public string ActGender { get; set; } = string.Empty; // "M" vai "F"
    }
}
