using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api._Shared.Dtos;
using cs_oppgave_05.Api.Directors.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Directors.Contracts
{
    /// <summary>
    /// Write endpoints for Directors (POST/PATCH/DELETE).
    /// </summary>
    public interface IDirectorWriteApi
    {
        /// <summary>Creates a new director.</summary>
        /// <remarks>POST /api/directors</remarks>
        Task<ActionResult<Director>> Create([FromBody] CreateDirectorDto dto);

        /// <summary>Partially updates a director by ID; null properties are ignored.</summary>
        /// <remarks>PATCH /api/directors/{id}</remarks>
        Task<IActionResult> Patch(int id, [FromBody] UpdateDirectorDto dto);

        /// <summary>Deletes a director by route ID.</summary>
        /// <remarks>DELETE /api/directors/{id}</remarks>
        Task<IActionResult> DeleteById(int id);

        /// <summary>Deletes a director by passing an ID DTO in the request body.</summary>
        /// <remarks>DELETE /api/directors</remarks>
        Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto);
    }
}
