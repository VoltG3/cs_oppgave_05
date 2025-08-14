using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers;
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesWriteControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_And_Persists()
    {
        using var db = CreateDb();
        var sut = new MoviesWriteController(db); 

        var dto = new CreateMovieDto
        {
            MovTitle = "New Movie",
            MovYear = 2024,
            MovTime = 123,
            MovLang = "EN",
            MovDtRel = null,
            MovRelCountry = "US"
        };

        var post = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);
        Assert.Equal("GetMovieById", created.RouteName); // 

        var entity = Assert.IsType<Movie>(created.Value);
        Assert.True(entity.MovId > 0);
        Assert.Equal("New Movie", entity.MovTitle);

        var fromDb = await db.Movies.FindAsync(entity.MovId);
        Assert.NotNull(fromDb);
        Assert.Equal("New Movie", fromDb!.MovTitle);
    }
    
}
