
namespace cs_oppgave_05.Api._Shared.Dtos.Contracts
{
    /// <summary>
    /// Contract for simple delete-by-id requests.
    /// </summary>
    public interface IDeleteByIdDto
    {
        /// <summary>
        /// Identifier of the entity to delete.
        /// </summary>
        int Id { get; }
    }
}
