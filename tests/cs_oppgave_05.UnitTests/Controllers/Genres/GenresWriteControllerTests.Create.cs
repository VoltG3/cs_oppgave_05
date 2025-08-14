using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Controllers; 
using cs_oppgave_05.Api.Genres.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public partial class GenresWriteControllerTests : GenresTestBase
{
    [Fact, Trait("Area","Genres"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_And_Persists()
    {
        using var db = CreateDb();
        var sut = new GenreWriteController(db);
        
        var dto = new CreateGenreDto { GenTitle = "Thriller" };
        var post = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);
        Assert.Equal("GetGenreById", created.RouteName);
        var entity = Assert.IsType<Entities.Genres>(created.Value);

        Assert.True(entity.GenId > 0);
        Assert.Equal("Thriller", entity.GenTitle);

        var fromDb = await db.Genres.FindAsync(entity.GenId);
        Assert.NotNull(fromDb);
        Assert.Equal("Thriller", fromDb!.GenTitle);
    }
    
}
