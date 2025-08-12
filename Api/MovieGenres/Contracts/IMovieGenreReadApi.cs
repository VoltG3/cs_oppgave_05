using Microsoft.AspNetCore.Mvc;

namespace cs_oppgave_05.Api.MovieGenres.Contracts
{
    /// <summary>
    /// Read-only endpoints for MovieGenres (GET operations).
    /// </summary>
    public interface IMovieGenresReadApi
    {
        /// <summary>
        /// Returns movie-genre links. If both movId and genId are provided, returns a single link.
        /// </summary>
        /// <remarks>
        /// REQUESTS:
        /// GET /api/movie_genres
        /// GET /api/movie_genres?movId=10&genId=3
        /// </remarks>
        Task<ActionResult<IEnumerable<Models.MovieGenres>>> GetAll(int? movId, int? genId);

        /// <summary>
        /// Returns a single movie-genre link by composite key.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/movie_genres/{movId}/{genId}
        /// Example: GET /api/movie_genres/10/3
        /// </remarks>
        Task<ActionResult<Models.MovieGenres>> GetById(int movId, int genId);
    }
}
