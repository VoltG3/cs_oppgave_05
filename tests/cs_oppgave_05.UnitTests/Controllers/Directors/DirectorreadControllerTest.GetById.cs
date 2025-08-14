using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Directors.Controllers;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public partial class DirectorsReadControllerTests : DirectorsTestBase
{
    [Fact, Trait("Area","Directors"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new DirectorsReadController(db);

        var res = await sut.GetById(999);
        Assert.IsType<NotFoundResult>(res.Result);
    }

    [Fact, Trait("Area","Directors"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok_For_Existing()
    {
        using var db = CreateDb();
        var seeded = SeedDirector(db, "Chris", "Nolan");
        var sut = new DirectorsReadController(db);

        var res = await sut.GetById(seeded.DirId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<Director>(ok.Value);

        Assert.Equal(seeded.DirId, dto.DirId);
        Assert.Equal("Chris", dto.DirFname);
        Assert.Equal("Nolan", dto.DirLname);
    }
    
}
