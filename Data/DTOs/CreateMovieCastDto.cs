
namespace cs_oppgave_05.Data.DTOs.MovieCasts
{
    public class CreateMovieCastDto
    {
        public int ActId { get; set; }
        public int MovId { get; set; }
        public string Role { get; set; } = null!;
    }
}
