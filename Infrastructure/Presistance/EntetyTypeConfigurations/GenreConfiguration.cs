using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class GenresConfiguration : IEntityTypeConfiguration<Genres>
{
    public void Configure(EntityTypeBuilder<Genres> b)
    {
        b.ToTable("Genres");
        b.HasKey(g => g.GenId);

        b.Property(g => g.GenTitle).IsRequired().HasMaxLength(100);
        b.HasIndex(g => g.GenTitle).IsUnique(); // ja vēlies unikālu nosaukumu
    }
}
