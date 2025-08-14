using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public partial class GenresReadControllerTests : GenresTestBase
{
    [Fact, Trait("Area","Genres"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new GenresReadController(db);

        var res = await sut.GetById(999);
        Assert.IsType<NotFoundResult>(res.Result);
    }

    [Fact, Trait("Area","Genres"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok_For_Existing()
    {
        using var db = CreateDb();
        var seeded = SeedGenre(db, "Action");
        var sut = new GenresReadController(db);
        
        var res = await sut.GetById(seeded.GenId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<Entities.Genres>(ok.Value);


        Assert.Equal(seeded.GenId, dto.GenId);
        Assert.Equal("Action", dto.GenTitle);
    }
    
}
