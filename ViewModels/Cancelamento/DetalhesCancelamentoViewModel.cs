namespace WebApplication3.ViewModels.Cancelamento
{
    public class DetalhesCancelamentoViewModel
    {
        public int Id { get; set; }
        public DateTime DataHoraNotificacao { get; set; }
        public string Motivo { get; set; } = String.Empty;
        public int VooId { get; set; }
    }
}
