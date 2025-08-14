using Microsoft.AspNetCore.Mvc; 
using cs_oppgave_05.Api.Directors.Controllers; 
using cs_oppgave_05.Api.Directors.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public partial class DirectorsWriteControllerTests : DirectorsTestBase
{
    [Fact, Trait("Area","Directors"), Trait("Action","Patch")]
    public async Task Patch_Updates_Names_When_Provided()
    {
        using var db = CreateDb();
        var d = SeedDirector(db, "OldF", "OldL");
        var sut = new DirectorsWriteController(db);

        var res = await sut.Patch(d.DirId, new UpdateDirectorDto { DirFname = "NewF", DirLname = "NewL" });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Directors.FindAsync(d.DirId);
        Assert.Equal("NewF", fromDb!.DirFname);
        Assert.Equal("NewL", fromDb!.DirLname);
    }

    [Fact, Trait("Area","Directors"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new DirectorsWriteController(db);

        var res = await sut.Patch(123456, new UpdateDirectorDto { DirFname = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
