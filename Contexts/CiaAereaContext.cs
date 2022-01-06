using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.EntityConfigurations;

namespace WebApplication3.Contexts
{
    public class CiaAereaContext: DbContext
    {
        public DbSet<Aeronave> Aeronaves { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Cancelamento> Cancelamentos { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }

        public CiaAereaContext(DbContextOptions<CiaAereaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AeronaveConfiguration());
            modelBuilder.ApplyConfiguration(new PilotoConfiguration());
            modelBuilder.ApplyConfiguration(new VooConfiguration());
            modelBuilder.ApplyConfiguration(new CancelamentoConfiguration());
            modelBuilder.ApplyConfiguration(new ManutencaoConfiguration());
        }
    }
}
