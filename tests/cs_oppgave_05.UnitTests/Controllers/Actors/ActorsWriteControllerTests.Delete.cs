using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Actors.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public partial class ActorsWriteControllerTests : ActorsTestBase
{
    [Fact, Trait("Area","Actors"), Trait("Action","Delete")]
    public async Task DeleteById_Removes_Entity()
    {
        using var db = CreateDb();
        var a = SeedActor(db, "To", "Remove", "M");
        var sut = new ActorsWriteController(db);

        var res = await sut.DeleteById(a.ActId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Actors.FindAsync(a.ActId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Actors"), Trait("Action","Delete")]
    public async Task DeleteById_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new ActorsWriteController(db);

        var res = await sut.DeleteById(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
