using System.Text.Json.Serialization;
using cs_oppgave_05.Api.Movies.Contracts;

namespace cs_oppgave_05.Api.Movies.Dtos
{
    public class UpdateMovieDetailsDto : IUpdateMovieDetailsDto
    {
        [JsonPropertyName("movTitle")] public string? MovTitle { get; set; }
        [JsonPropertyName("movYear")] public int? MovYear { get; set; }
        [JsonPropertyName("movTime")] public int? MovTime { get; set; }
        [JsonPropertyName("movLang")] public string? MovLang { get; set; }
        [JsonPropertyName("movDtRel")] public DateTime? MovDtRel { get; set; }
        [JsonPropertyName("movRelCountry")] public string? MovRelCountry { get; set; }

        [JsonPropertyName("genres")] public List<UpdateGenreDto>? Genres { get; set; }
        [JsonPropertyName("directors")] public List<UpdateDirectorDto>? Directors { get; set; }
        [JsonPropertyName("cast")] public List<UpdateCastDto>? Cast { get; set; }
        [JsonPropertyName("ratings")] public List<UpdateRatingDto>? Ratings { get; set; }

        IReadOnlyList<IUpdateGenreDto>? IUpdateMovieDetailsDto.Genres => Genres;
        IReadOnlyList<IUpdateDirectorDto>? IUpdateMovieDetailsDto.Directors => Directors;
        IReadOnlyList<IUpdateCastDto>? IUpdateMovieDetailsDto.Cast => Cast;
        IReadOnlyList<IUpdateRatingDto>? IUpdateMovieDetailsDto.Ratings => Ratings;
    }

    public record UpdateGenreDto(int GenId, string? GenTitle) : IUpdateGenreDto;
    public record UpdateDirectorDto(int DirId, string? DirFname, string? DirLname) : IUpdateDirectorDto;
    public record UpdateCastDto(int ActId, string? ActFname, string? ActLname, string? Role) : IUpdateCastDto;
    public record UpdateRatingDto(int RevId, decimal? RevStars, int? NumOfRatings) : IUpdateRatingDto;
}
