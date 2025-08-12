
namespace cs_oppgave_05.Api.MovieDirections.Dtos.Contracts
{
    /// <summary>
    /// Marker contract for update requests. MovieDirection has no updatable fields.
    /// PATCH should typically return 400 Bad Request.
    /// </summary>
    public interface IUpdateMovieDirectionDto { }
}
