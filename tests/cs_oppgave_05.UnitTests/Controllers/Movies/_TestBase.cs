using Microsoft.Data.Sqlite; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public abstract class MoviesTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"MoviesDb_{Guid.NewGuid()}")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)) 
            .Options;
        return new AppDbContext(opts);
    }
    
    protected AppDbContext CreateRelationalDb()
    {
        var conn = new SqliteConnection("Data Source=:memory:");
        conn.Open(); 

        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(conn)
            .Options;

        var db = new AppDbContext(opts);
        db.Database.EnsureCreated(); 
        return db;
    }

    protected Movie SeedMovie(AppDbContext db, string title = "Seeded", int year = 2000)
    {
        var m = new Movie { MovTitle = title, MovYear = year };
        db.Movies.Add(m);
        db.SaveChanges();
        return m;
    }
    
    protected void SeedRelationsFor(AppDbContext db, Movie m)
    {
        var g = new Entities.Genres   { GenTitle = "Thriller" };
        var d = new Director { DirFname = "Jane", DirLname = "Doe" };
        var a = new Actor    { ActFname = "Bob",  ActLname = "Star", ActGender = "M" };
        var r = new Reviewer { RevName  = "Critic" };

        db.Genres.Add(g);
        db.Directors.Add(d);
        db.Actors.Add(a);
        db.Reviewers.Add(r);
        db.SaveChanges();

        db.MovieGenres.Add(new Entities.MovieGenres   { MovId = m.MovId, GenId = g.GenId });
        db.MovieDirections.Add(new MovieDirection { MovId = m.MovId, DirId = d.DirId });
        db.MovieCasts.Add(new MovieCast      { MovId = m.MovId, ActId = a.ActId, Role = "Role" });
        db.Ratings.Add(new Rating            { MovId = m.MovId, RevId = r.RevId, RevStars = 5, NumOfRatings = 1 });
        db.SaveChanges();
    }
    
}
