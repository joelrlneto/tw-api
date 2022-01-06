using WebApplication3.Entities.Enums;

namespace WebApplication3.Entities
{
    public class Manutencao
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public TipoManutencao Tipo { get; set; }
        public string Observacoes { get; set; }
        public int AeronaveId { get; set; }
        public Aeronave Aeronave { get; set; }
    }
}
