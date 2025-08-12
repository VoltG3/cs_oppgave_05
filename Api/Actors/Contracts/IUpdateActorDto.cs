
namespace cs_oppgave_05.Api.Actors.Dtos.Contracts
{
    /// <summary>
    /// Contract for partial actor updates (PATCH /api/actors/{id}). Nulls mean "no change".
    /// </summary>
    public interface IUpdateActorDto
    {
        /// <summary>Actor first name (optional).</summary>
        string? ActFname { get; }

        /// <summary>Actor last name (optional).</summary>
        string? ActLname { get; }

        /// <summary>Actor gender, e.g. "M" or "F" (optional).</summary>
        string? ActGender { get; }
    }
}
