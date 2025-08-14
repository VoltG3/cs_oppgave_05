using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public abstract class GenresTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"GenresDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Entities.Genres SeedGenre(AppDbContext db, string title = "Seeded Genre")
    {
        var g = new Entities.Genres { GenTitle = title };
        db.Genres.Add(g);
        db.SaveChanges();
        return g;
    }

    protected List<Entities.Genres> SeedGenres(AppDbContext db, params string[] titles)
    {
        var list = new List<Entities.Genres>();
        foreach (var t in titles)
        {
            var g = new Entities.Genres { GenTitle = t };
            db.Genres.Add(g);
            list.Add(g);
        }

        db.SaveChanges();
        return list;
    }
    
}
