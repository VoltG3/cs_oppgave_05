
using cs_oppgave_05.Api.Movies.Dtos.Contracts;

namespace cs_oppgave_05.Api.Movies.Dtos
{
    // Read-only details projection for Movies (used by /api/movies/{id}/details)
    public record MovieDetailsDto(
        int MovId,
        string MovTitle,
        int? MovYear,
        int? MovTime,
        string? MovLang,
        DateTime? MovDtRel,
        string MovRelCountry,
        List<GenreDto> Genres,
        List<DirectorDto> Directors,
        List<CastDto> Cast,
        List<RatingDto> Ratings
    ) : IMovieDetailsDto
    {
        // Explicit interface members to satisfy IReadOnlyList<I...Dto> from the interface
        IReadOnlyList<IGenreDto> IMovieDetailsDto.Genres => Genres;
        IReadOnlyList<IDirectorDto> IMovieDetailsDto.Directors => Directors;
        IReadOnlyList<ICastDto> IMovieDetailsDto.Cast => Cast;
        IReadOnlyList<IRatingDto> IMovieDetailsDto.Ratings => Ratings;
    }

    // Child DTOs implementing their respective interfaces
    public record GenreDto(int GenId, string GenTitle) : IGenreDto;
    public record DirectorDto(int DirId, string DirFname, string DirLname) : IDirectorDto;
    public record CastDto(int ActId, string ActFname, string ActLname, string? Role) : ICastDto;
    public record RatingDto(int RevId, decimal? RevStars, int? NumOfRatings) : IRatingDto;
}
