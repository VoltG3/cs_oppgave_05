using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Reviewers.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public partial class ReviewersWriteControllerTests : ReviewersTestBase
{
    [Fact, Trait("Area","Reviewers"), Trait("Action","Delete")]
    public async Task DeleteById_Removes_Entity()
    {
        using var db = CreateDb();
        var r = SeedReviewer(db, "ToDelete");
        var sut = new ReviewersWriteController(db);

        var res = await sut.DeleteById(r.RevId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Reviewers.FindAsync(r.RevId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Reviewers"), Trait("Action","Delete")]
    public async Task DeleteById_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new ReviewersWriteController(db);

        var res = await sut.DeleteById(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
