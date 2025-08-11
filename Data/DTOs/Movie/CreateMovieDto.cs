
namespace cs_oppgave_05.Data.DTOs.Movies
{
    public class CreateMovieDto
    {
        public string MovTitle { get; set; } = string.Empty;
        public int? MovYear { get; set; }
        public int? MovTime { get; set; }
        public string? MovLang { get; set; }
        public DateTime? MovDtRel { get; set; }
        public string? MovRelCountry { get; set; }
    }
}
