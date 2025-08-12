
namespace cs_oppgave_05.Api.Directors.Dtos.Contracts
{
    /// <summary>
    /// Contract for partial director updates (PATCH /api/directors/{id}). Nulls mean "no change".
    /// </summary>
    public interface IUpdateDirectorDto
    {
        /// <summary>First name (optional).</summary>
        string? DirFname { get; }

        /// <summary>Last name (optional).</summary>
        string? DirLname { get; }
    }
}
