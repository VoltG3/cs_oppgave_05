using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Reviewers.Controllers;
using cs_oppgave_05.Api.Reviewers.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public partial class ReviewersWriteControllerTests : ReviewersTestBase
{
    [Fact, Trait("Area","Reviewers"), Trait("Action","Patch")]
    public async Task Patch_Updates_Name_When_Provided()
    {
        using var db = CreateDb();
        var r = SeedReviewer(db, "Old Name");
        var sut = new ReviewersWriteController(db);

        var res = await sut.Patch(r.RevId, new UpdateReviewerDto { RevName = "New Name" });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Reviewers.FindAsync(r.RevId);
        Assert.Equal("New Name", fromDb!.RevName);
    }

    [Fact, Trait("Area","Reviewers"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new ReviewersWriteController(db);

        var res = await sut.Patch(123456, new UpdateReviewerDto { RevName = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
