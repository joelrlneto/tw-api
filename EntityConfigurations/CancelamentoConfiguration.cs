using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Entities;

namespace WebApplication3.EntityConfigurations
{
    public class CancelamentoConfiguration : IEntityTypeConfiguration<Cancelamento>
    {
        public void Configure(EntityTypeBuilder<Cancelamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Motivo)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.DataHoraNotificacao)
                   .IsRequired();
        }
    }
}
