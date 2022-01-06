using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Entities;

namespace WebApplication3.EntityConfigurations
{
    public class PilotoConfiguration : IEntityTypeConfiguration<Piloto>
    {
        public void Configure(EntityTypeBuilder<Piloto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Matricula)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasIndex(p => p.Matricula)
                   .IsUnique();

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
