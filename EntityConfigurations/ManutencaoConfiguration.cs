using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Entities;

namespace WebApplication3.EntityConfigurations
{
    public class ManutencaoConfiguration : IEntityTypeConfiguration<Manutencao>
    {
        public void Configure(EntityTypeBuilder<Manutencao> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.DataHora)
                   .IsRequired();

            builder.Property(m => m.Tipo)
                   .IsRequired();

            builder.Property(m => m.Observacoes)
                   .HasMaxLength(100);
        }
    }
}
