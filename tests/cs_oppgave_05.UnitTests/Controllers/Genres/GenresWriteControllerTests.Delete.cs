using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public partial class GenresWriteControllerTests : GenresTestBase
{
    [Fact, Trait("Area","Genres"), Trait("Action","Delete")]
    public async Task DeleteById_Removes_Entity()
    {
        using var db = CreateDb();
        var g = SeedGenre(db, "ToRemove");
        var sut = new GenreWriteController(db);
        
        var res = await sut.DeleteById(g.GenId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Genres.FindAsync(g.GenId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Genres"), Trait("Action","Delete")]
    public async Task DeleteById_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new GenreWriteController(db);
        
        var res = await sut.DeleteById(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
