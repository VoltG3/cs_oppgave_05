using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesReadControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MoviesReadController(db);

        var res = await sut.GetById(999);
        Assert.IsType<NotFoundResult>(res.Result);
    }

    [Fact, Trait("Area","Movies"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok_For_Existing()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Matrix", 1999);
        var sut = new MoviesReadController(db);

        var res = await sut.GetById(m.MovId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<cs_oppgave_05.Entities.Movie>(ok.Value);

        Assert.Equal(m.MovId, dto.MovId);
        Assert.Equal("Matrix", dto.MovTitle);
        Assert.Equal(1999, dto.MovYear);
    }
    
}
