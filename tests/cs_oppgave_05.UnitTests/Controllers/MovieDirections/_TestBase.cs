using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public abstract class MovieDirectionTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"MovieDirectionDb_{Guid.NewGuid()}")
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

    protected Director SeedDirector(AppDbContext db, string fname = "John", string lname = "Doe")
    {
        var d = new Director { DirFname = fname, DirLname = lname };
        db.Directors.Add(d);
        db.SaveChanges();
        return d;
    }

    protected Entities.MovieDirection Link(AppDbContext db, int movId, int dirId)
    {
        var md = new Entities.MovieDirection { MovId = movId, DirId = dirId };
        db.MovieDirections.Add(md);
        db.SaveChanges();
        return md;
    }
    
}
