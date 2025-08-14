using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Directors.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public partial class DirectorsWriteControllerTests : DirectorsTestBase
{
    [Fact, Trait("Area","Directors"), Trait("Action","Delete")]
    public async Task DeleteById_Removes_Entity()
    {
        using var db = CreateDb();
        var d = SeedDirector(db, "To", "Remove");
        var sut = new DirectorsWriteController(db);

        var res = await sut.DeleteById(d.DirId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Directors.FindAsync(d.DirId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Directors"), Trait("Action","Delete")]
    public async Task DeleteById_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new DirectorsWriteController(db);

        var res = await sut.DeleteById(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
