
namespace cs_oppgave_05.Data.DTOs.Movie;

public record MovieDetailsDto(
    int MovId,
    string MovTitle,
    int? MovYear,
    int? MovTime,
    string? MovLang,
    DateTime? MovDtRel,
    string MovRelCountry,
    List<string> Genres,
    List<DirectorDto> Directors,
    List<CastDto> Cast,
    List<RatingDto> Ratings
);

public record DirectorDto(int DirId, string DirFname, string DirLname);
public record CastDto(int ActId, string ActFname, string ActLname, string? Role);
public record RatingDto(int RevId, decimal? RevStars, int? NumOfRatings);

