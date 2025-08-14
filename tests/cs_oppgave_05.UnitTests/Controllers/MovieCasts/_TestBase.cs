using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public abstract class MovieCastsTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"MovieCastsDb_{Guid.NewGuid()}")
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

    protected Actor SeedActor(AppDbContext db, string fname = "John", string lname = "Doe", string gender = "M")
    {
        var a = new Actor { ActFname = fname, ActLname = lname, ActGender = gender };
        db.Actors.Add(a);
        db.SaveChanges();
        return a;
    }

    protected MovieCast Link(AppDbContext db, int movId, int actId, string role = "Role")
    {
        var mc = new MovieCast { MovId = movId, ActId = actId, Role = role };
        db.MovieCasts.Add(mc);
        db.SaveChanges();
        return mc;
    }
    
}
