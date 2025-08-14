using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieCasts.Controllers; 
using cs_oppgave_05.Api.MovieCasts.Dtos; 
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public partial class MovieCastsWriteControllerTests : MovieCastsTestBase
{
    [Fact, Trait("Area","MovieCasts"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_When_Movie_And_Actor_Exist()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "New", 2024);
        var a = SeedActor(db, "Greta", "Gerwig", "F");
        var sut = new MovieCastsWriteController(db);

        var dto = new CreateMovieCastDto { MovId = m.MovId, ActId = a.ActId, Role = "Lead" };
        var res = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(res.Result);

        Assert.Equal("GetMovieCastById", created.RouteName);
        var entity = Assert.IsType<MovieCast>(created.Value);
        Assert.Equal(m.MovId, entity.MovId);
        Assert.Equal(a.ActId, entity.ActId);
        Assert.Equal("Lead", entity.Role);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","Create")]
    public async Task Create_Returns_BadRequest_When_Movie_Or_Actor_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsWriteController(db);

        // Movie missing, Actor exists
        var a = SeedActor(db, "Only", "Actor", "M");
        var bad1 = await sut.Create(new CreateMovieCastDto { MovId = 999, ActId = a.ActId, Role = "X" });
        Assert.IsType<BadRequestObjectResult>(bad1.Result);

        // Actor missing, Movie exists
        var m = SeedMovie(db, "OnlyMovie", 2001);
        var bad2 = await sut.Create(new CreateMovieCastDto { MovId = m.MovId, ActId = 999, Role = "Y" });
        Assert.IsType<BadRequestObjectResult>(bad2.Result);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","Create")]
    public async Task Create_Returns_Conflict_When_Duplicate_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Dup", 2000);
        var a = SeedActor(db, "Dup", "Act", "F");
        Link(db, m.MovId, a.ActId, "OldRole");

        var sut = new MovieCastsWriteController(db);
        var res = await sut.Create(new CreateMovieCastDto { MovId = m.MovId, ActId = a.ActId, Role = "NewRole" });

        Assert.IsType<ConflictObjectResult>(res.Result);
    }
    
}
