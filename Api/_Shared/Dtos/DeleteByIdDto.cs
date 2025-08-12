
using cs_oppgave_05.Api._Shared.Dtos.Contracts;

namespace cs_oppgave_05.Api._Shared.Dtos
{
    public sealed class DeleteByIdDto : IDeleteByIdDto
    {
        public int Id { get; set; }
    }
}
