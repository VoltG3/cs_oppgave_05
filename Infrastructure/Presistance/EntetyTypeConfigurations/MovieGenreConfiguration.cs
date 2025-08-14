using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Infrastructure.Presistance.EntetyTypeConfigurations
{
    public class MovieGenresConfiguration : IEntityTypeConfiguration<MovieGenres>
    {
        public void Configure(EntityTypeBuilder<MovieGenres> b)
        {
            b.ToTable("movie_genres");
            b.HasKey(mg => new { mg.MovId, mg.GenId });

            b.HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(mg => mg.Genres)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
