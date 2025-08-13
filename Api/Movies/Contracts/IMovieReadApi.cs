using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Entities;
    
namespace cs_oppgave_05.Api.Movies.Contracts
{
    /// <summary>
    /// Read-only endpoints for Movies (GET operations).
    /// </summary>
    public interface IMovieReadApi
    {
        /// <summary>
        /// Returns all movies.
        /// </summary>
        /// <remarks>
        /// REQUEST (browser-friendly):
        /// GET /api/movies
        /// </remarks>
        Task<ActionResult<IEnumerable<Movie>>> GetAll();

        /// <summary>
        /// Returns a single movie by its ID.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/movies/{id}
        /// Example: GET /api/movies/42
        /// </remarks>
        Task<ActionResult<Movie>> GetById(int id);

        /// <summary>
        /// Returns detailed info for a movie, with optional related data.
        /// </summary>
        /// <remarks>
        /// REQUEST:
        /// GET /api/movies/{id}/details?include={parts}
        /// - include: comma-separated values: genres,directors,cast,ratings
        /// - If omitted/empty, all parts are included.
        /// Examples:
        ///   GET /api/movies/42/details
        ///   GET /api/movies/42/details?include=genres,cast
        /// </remarks>
        Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id, string? include);
    }
}
