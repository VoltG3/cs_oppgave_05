using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesWriteControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","Delete")]
    public async Task DeleteById_Removes_Entity()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "ToRemove", 2010);
        var sut = new MoviesWriteController(db);

        var res = await sut.DeleteById(m.MovId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Movies.FindAsync(m.MovId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Movies"), Trait("Action","Delete")]
    public async Task DeleteById_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new MoviesWriteController(db);

        var res = await sut.DeleteById(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
    
}