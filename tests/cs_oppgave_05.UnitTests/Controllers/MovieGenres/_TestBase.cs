using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public abstract class MovieGenresTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"MovieGenresDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Movie SeedMovie(AppDbContext db, string title = "Seeded", int year = 2000)
    {
        var m = new Movie { MovTitle = title, MovYear = year };
        db.Movies.Add(m);
        db.SaveChanges();
        return m;
    }

    protected Entities.Genres SeedGenre(AppDbContext db, string title = "Drama")
    {
        var g = new Entities.Genres { GenTitle = title };
        db.Genres.Add(g);
        db.SaveChanges();
        return g;
    }

    protected Entities.MovieGenres Link(AppDbContext db, int movId, int genId)
    {
        var mg = new Entities.MovieGenres { MovId = movId, GenId = genId };
        db.MovieGenres.Add(mg);
        db.SaveChanges();
        return mg;
    }
    
}
