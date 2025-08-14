using Microsoft.AspNetCore.Mvc;

namespace cs_oppgave_05.Api.Movies.Contracts
{
    public interface IDeleteMovieApi
    {
        /// <summary>
        /// Deletes only the movie (without related records)
        /// </summary>
        Task<IActionResult> DeleteMovieOnly(int id);

        /// <summary>
        /// Deletes the movie and ALL related records
        /// </summary>
        Task<IActionResult> DeleteMovieWithAllRelations(int id);
    }
}
