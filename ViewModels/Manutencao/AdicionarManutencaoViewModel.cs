using WebApplication3.Entities.Enums;

namespace WebApplication3.ViewModels.Manutencao
{
    public class AdicionarManutencaoViewModel
    {
        public DateTime DataHora { get; set; }
        public TipoManutencao Tipo { get; set; }
        public string? Observacoes { get; set; }
        public int AeronaveId { get; set; }
    }
}
