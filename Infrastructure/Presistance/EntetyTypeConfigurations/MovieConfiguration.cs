using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Infrastructure.Presistance.EntetyTypeConfigurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> b)
        {
            b.ToTable("movie");

            b.HasKey(m => m.MovId);

            b.Property(m => m.MovTitle)
                .IsRequired()
                .HasMaxLength(255);

            b.Property(m => m.MovLang)
                .HasMaxLength(50);

            // Store Date without Time
            b.Property(m => m.MovDtRel)
                .HasColumnType("date");

            b.Property(m => m.MovRelCountry)
                .HasMaxLength(64);

            b.HasIndex(m => new { m.MovTitle, m.MovYear });
        }
    }
}
