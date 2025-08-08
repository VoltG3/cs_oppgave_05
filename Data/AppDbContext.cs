using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data;
    
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Director> Directors { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<MovieDirection> MovieDirections { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieGenres> MovieGenres { get; set; }
    public DbSet<Genres> Genres { get; set; }
    
    // Relations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // movie_direction : movie | director
        
        modelBuilder.Entity<MovieDirection>()
            .HasKey(md => new { md.DirId, md.MovId });

        modelBuilder.Entity<MovieDirection>()
            .HasOne(md => md.Director)
            .WithMany(d => d.MovieDirections)
            .HasForeignKey(md => md.DirId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MovieDirection>()
            .HasOne(md => md.Movie)
            .WithMany(m => m.MovieDirections)
            .HasForeignKey(md => md.MovId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // movie_cast : movie | actor
        
        modelBuilder.Entity<MovieCast>()
            .HasKey(mk => new { mk.ActId, mk.MovId });
        
        modelBuilder.Entity<MovieCast>()
            .HasOne(mk => mk.Movie)
            .WithMany(m => m.MovieCasts)
            .HasForeignKey(mk => mk.MovId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<MovieCast>()
            .HasOne(mk => mk.Actor)
            .WithMany(a => a.MovieCasts)
            .HasForeignKey(mk => mk.ActId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // movie_genres : movie | genres
        
        modelBuilder.Entity<MovieGenres>()
            .HasKey(mg => new { mg.MovId, mg.GenId });
        
        modelBuilder.Entity<MovieGenres>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.MovieGenres)
            .HasForeignKey(mg => mg.MovId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<MovieGenres>()
            .HasOne(mg => mg.Genres)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // rating : movie | reviewer
        
        modelBuilder.Entity<Rating>()
            .HasKey(r => new { r.MovId, r.RevId });
        
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Ratings)
            .HasForeignKey(r => r.MovId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Reviewer)
            .WithMany(r => r.Ratings)
            .HasForeignKey(r => r.RevId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
