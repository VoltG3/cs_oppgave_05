
namespace cs_oppgave_05.Api.Directors.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating a director (request body for POST /api/directors).
    /// </summary>
    public interface ICreateDirectorDto
    {
        /// <summary>Director first name.</summary>
        string DirFname { get; }

        /// <summary>Director last name.</summary>
        string DirLname { get; }
    }
}
