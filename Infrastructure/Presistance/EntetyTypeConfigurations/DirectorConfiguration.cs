using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> b)
    {
        b.ToTable("director");
        b.HasKey(d => d.DirId);

        b.Property(d => d.DirFname).IsRequired().HasMaxLength(100);
        b.Property(d => d.DirLname).IsRequired().HasMaxLength(100);
    }
}
