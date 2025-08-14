using cs_oppgave_05.Api.Movies.Dtos.Contracts;

namespace cs_oppgave_05.Api.Movies.Contracts
{
    public interface IUpdateMovieDetailsDto : IUpdateMovieDto
    {
        IReadOnlyList<IUpdateGenreDto>? Genres { get; }
        IReadOnlyList<IUpdateDirectorDto>? Directors { get; }
        IReadOnlyList<IUpdateCastDto>? Cast { get; }
        IReadOnlyList<IUpdateRatingDto>? Ratings { get; }
    }

    public interface IUpdateGenreDto
    {
        int GenId { get; }
        string? GenTitle { get; }
    }

    public interface IUpdateDirectorDto
    {
        int DirId { get; }
        string? DirFname { get; }
        string? DirLname { get; }
    }

    public interface IUpdateCastDto
    {
        int ActId { get; }
        string? ActFname { get; }
        string? ActLname { get; }
        string? Role { get; }
    }

    public interface IUpdateRatingDto
    {
        int RevId { get; }
        decimal? RevStars { get; }
        int? NumOfRatings { get; }
    }
}
