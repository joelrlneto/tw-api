using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.EntityConfigurations;

namespace WebApplication3.Contexts
{
    public class CiaAereaContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public CiaAereaContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

#nullable disable
        public DbSet<Aeronave> Aeronaves { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Cancelamento> Cancelamentos { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
#nullable restore

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CiaAerea"));
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
