
namespace cs_oppgave_05.Api.Movies.Dtos.Contracts
{
    /// <summary>
    /// Read-only projection contract returned by GET /api/movies/{id}/details.
    /// </summary>
    public interface IMovieDetailsDto
    {
        /// <summary>Movie identifier.</summary>
        int MovId { get; }

        /// <summary>Movie title.</summary>
        string MovTitle { get; }

        /// <summary>Production/release year (optional).</summary>
        int? MovYear { get; }

        /// <summary>Runtime in minutes (optional).</summary>
        int? MovTime { get; }

        /// <summary>Original language (optional).</summary>
        string? MovLang { get; }

        /// <summary>Official release date (optional).</summary>
        DateTime? MovDtRel { get; }

        /// <summary>Release country.</summary>
        string MovRelCountry { get; }

        /// <summary>Associated genres (optional list depending on include=).</summary>
        IReadOnlyList<IGenreDto> Genres { get; }

        /// <summary>Associated directors (optional list depending on include=).</summary>
        IReadOnlyList<IDirectorDto> Directors { get; }

        /// <summary>Associated cast (optional list depending on include=).</summary>
        IReadOnlyList<ICastDto> Cast { get; }

        /// <summary>Associated ratings (optional list depending on include=).</summary>
        IReadOnlyList<IRatingDto> Ratings { get; }
    }

    /// <summary>Genre projection.</summary>
    public interface IGenreDto
    {
        /// <summary>Genre identifier.</summary>
        int GenId { get; }

        /// <summary>Genre title.</summary>
        string GenTitle { get; }
    }

    /// <summary>Director projection.</summary>
    public interface IDirectorDto
    {
        /// <summary>Director identifier.</summary>
        int DirId { get; }

        /// <summary>Director first name.</summary>
        string DirFname { get; }

        /// <summary>Director last name.</summary>
        string DirLname { get; }
    }

    /// <summary>Cast projection (actor + role).</summary>
    public interface ICastDto
    {
        /// <summary>Actor identifier.</summary>
        int ActId { get; }

        /// <summary>Actor first name.</summary>
        string ActFname { get; }

        /// <summary>Actor last name.</summary>
        string ActLname { get; }

        /// <summary>Role name (optional).</summary>
        string? Role { get; }
    }

    /// <summary>Rating projection.</summary>
    public interface IRatingDto
    {
        /// <summary>Reviewer identifier.</summary>
        int RevId { get; }

        /// <summary>Star rating value (optional).</summary>
        decimal? RevStars { get; }

        /// <summary>Number of ratings (optional).</summary>
        int? NumOfRatings { get; }
    }
}
