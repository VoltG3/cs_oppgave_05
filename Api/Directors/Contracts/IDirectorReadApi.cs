using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Directors.Contracts
{
    /// <summary>
    /// Read-only endpoints for Directors (GET operations).
    /// </summary>
    public interface IDirectorReadApi
    {
        /// <summary>Returns all directors.</summary>
        /// <remarks>GET /api/directors</remarks>
        Task<ActionResult<IEnumerable<Director>>> GetAll();

        /// <summary>Returns a single director by its ID.</summary>
        /// <remarks>GET /api/directors/{id}</remarks>
        Task<ActionResult<Director>> GetById(int id);
    }
}
