using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Actors.Controllers; 
using cs_oppgave_05.Api.Actors.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public partial class ActorsWriteControllerTests : ActorsTestBase
{
    [Fact, Trait("Area","Actors"), Trait("Action","Patch")]
    public async Task Patch_Updates_Fields_When_Provided()
    {
        using var db = CreateDb();
        var a = SeedActor(db, "OldF", "OldL", "M");
        var sut = new ActorsWriteController(db);

        var res = await sut.Patch(a.ActId, new UpdateActorDto { ActFname = "NewF", ActLname = "NewL", ActGender = "F" });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Actors.FindAsync(a.ActId);
        Assert.Equal("NewF", fromDb!.ActFname);
        Assert.Equal("NewL", fromDb!.ActLname);
        Assert.Equal("F", fromDb!.ActGender);
    }

    [Fact, Trait("Area","Actors"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new ActorsWriteController(db);

        var res = await sut.Patch(123456, new UpdateActorDto { ActFname = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
