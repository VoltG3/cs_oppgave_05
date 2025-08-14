using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Reviewers.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public partial class ReviewersReadControllerTests : ReviewersTestBase
{
    [Fact, Trait("Area","Reviewers"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new ReviewersReadController(db);

        var res = await sut.GetById(999);
        Assert.IsType<NotFoundResult>(res.Result);
    }

    [Fact, Trait("Area","Reviewers"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok_For_Existing()
    {
        using var db = CreateDb();
        var seeded = SeedReviewer(db, "X");
        var sut = new ReviewersReadController(db);

        var res = await sut.GetById(seeded.RevId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<cs_oppgave_05.Entities.Reviewer>(ok.Value);

        Assert.Equal(seeded.RevId, dto.RevId);
        Assert.Equal("X", dto.RevName);
    }
    
}
