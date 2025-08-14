using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 
using cs_oppgave_05.Entities; 
namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesReadControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        SeedMovie(db, "A", 1999);
        SeedMovie(db, "B", 2001);
        SeedMovie(db, "C", 2005);

        var sut = new MoviesReadController(db); // :contentReference[oaicite:1]{index=1}

        var result = await sut.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Movie>>(ok.Value);

        Assert.Equal(3, list.Count());
    }
    
}
