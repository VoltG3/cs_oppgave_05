
namespace cs_oppgave_05.Api.Movies.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a movie (request body for POST /api/movies).
    /// </summary>
    public interface ICreateMovieDto
    {
        /// <summary>Movie title.</summary>
        string MovTitle { get; }

        /// <summary>Production/release year (optional).</summary>
        int? MovYear { get; }

        /// <summary>Runtime in minutes (optional).</summary>
        int? MovTime { get; }

        /// <summary>Original language (optional).</summary>
        string? MovLang { get; }

        /// <summary>Official release date (optional, ISO-8601).</summary>
        DateTime? MovDtRel { get; }

        /// <summary>Release country code/name (optional).</summary>
        string? MovRelCountry { get; }
    }
}
