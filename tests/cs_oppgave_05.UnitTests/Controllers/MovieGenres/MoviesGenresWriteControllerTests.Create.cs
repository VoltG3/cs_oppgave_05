using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Controllers;
using cs_oppgave_05.Api.MovieGenres.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public partial class MovieGenresWriteControllerTests : MovieGenresTestBase
{
    [Fact, Trait("Area","MovieGenres"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_When_Movie_And_Genre_Exist()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "New", 2024);
        var g = SeedGenre(db, "Sci-Fi");
        var sut = new MovieGenresWriteController(db);

        var dto = new CreateMovieGenresDto { MovId = m.MovId, GenId = g.GenId };
        var res = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(res.Result);

        Assert.Equal("GetMovieGenreById", created.RouteName);
        var entity = Assert.IsType<Entities.MovieGenres>(created.Value);
        Assert.Equal(m.MovId, entity.MovId);
        Assert.Equal(g.GenId, entity.GenId);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","Create")]
    public async Task Create_Returns_BadRequest_When_Movie_Or_Genre_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieGenresWriteController(db);

        // Movie missing, Genre exists
        var g = SeedGenre(db, "OnlyGenre");
        var bad1 = await sut.Create(new CreateMovieGenresDto { MovId = 999, GenId = g.GenId });
        Assert.IsType<BadRequestObjectResult>(bad1.Result);

        // Genre missing, Movie exists
        var m = SeedMovie(db, "OnlyMovie", 2001);
        var bad2 = await sut.Create(new CreateMovieGenresDto { MovId = m.MovId, GenId = 999 });
        Assert.IsType<BadRequestObjectResult>(bad2.Result);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","Create")]
    public async Task Create_Returns_Conflict_When_Duplicate_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Dup", 2000);
        var g = SeedGenre(db, "DupGen");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresWriteController(db);
        var res = await sut.Create(new CreateMovieGenresDto { MovId = m.MovId, GenId = g.GenId });

        Assert.IsType<ConflictObjectResult>(res.Result);
    }
    
}
