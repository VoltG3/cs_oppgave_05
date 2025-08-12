
namespace cs_oppgave_05.Api.Genres.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a genre (request body for POST /api/genres).
    /// </summary>
    public interface ICreateGenreDto
    {
        /// <summary>Genre title.</summary>
        string GenTitle { get; }
    }
}
