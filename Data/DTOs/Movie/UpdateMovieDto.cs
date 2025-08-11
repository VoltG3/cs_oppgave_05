
namespace cs_oppgave_05.Data.DTOs.Movies
{
    public class UpdateMovieDto
    {
        public string? MovTitle { get; set; }
        public int? MovYear { get; set; }
        public int? MovTime { get; set; }
        public string? MovLang { get; set; }
        public DateTime? MovDtRel { get; set; }
        public string? MovRelCountry { get; set; }
    }
}
