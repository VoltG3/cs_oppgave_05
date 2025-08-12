
namespace cs_oppgave_05.Api.Genres.Dtos.Contracts
{
    /// <summary>
    /// Contract for partial genre updates (PATCH /api/genres/{id}). Nulls mean "no change".
    /// </summary>
    public interface IUpdateGenreDto
    {
        /// <summary>Genre title (optional).</summary>
        string? GenTitle { get; }
    }
}
