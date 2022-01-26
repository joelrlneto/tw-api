namespace WebApplication3.ViewModels.Cancelamento
{
    public class AtualizarCancelamentoViewModel
    {
        public int Id { get; set; }
        public DateTime DataHoraNotificacao { get; set; }
        public string Motivo { get; set; } = String.Empty;
    }
}
