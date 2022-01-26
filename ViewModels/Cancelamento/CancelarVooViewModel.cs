namespace WebApplication3.ViewModels.Cancelamento
{
    public class CancelarVooViewModel
    {
        public DateTime DataHoraNotificacao { get; set; }
        public string Motivo { get; set; } = String.Empty;
        public int VooId { get; set; }
    }
}
