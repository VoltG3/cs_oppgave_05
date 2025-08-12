
namespace cs_oppgave_05.Api.Actors.Dtos.Contracts
{
    /// <summary>
    /// Contract for creating an actor (request body for POST /api/actors).
    /// </summary>
    public interface ICreateActorDto
    {
        /// <summary>Actor first name.</summary>
        string ActFname { get; }

        /// <summary>Actor last name.</summary>
        string ActLname { get; }

        /// <summary>Actor gender, e.g. "M" or "F".</summary>
        string ActGender { get; }
    }
}
