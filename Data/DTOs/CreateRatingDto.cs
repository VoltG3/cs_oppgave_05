
namespace cs_oppgave_05.Data.DTOs.Rating
{
    public class CreateRatingDto
    {
        public int MovId { get; set; }
        public int RevId { get; set; }
        public decimal? RevStars { get; set; }
        public int? NumOfRatings { get; set; }
    }
}

