using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Dtos;

namespace cs_oppgave_05.Api.Movies.Contracts
{
    public interface IMovieDetailsWriteApi
    {
        /// <summary>
        /// Updates movie details including related entities.
        /// </summary>
        Task<IActionResult> PatchDetails(int id, [FromBody] UpdateMovieDetailsDto dto);
    }
}
