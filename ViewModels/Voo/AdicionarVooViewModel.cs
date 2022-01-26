namespace WebApplication3.ViewModels.Voo
{
    public class AdicionarVooViewModel
    {
        public string Origem { get; set; } = String.Empty;
        public string Destino { get; set; } = String.Empty;
        public DateTime DataHoraPartida { get; set; }
        public DateTime DataHoraChegada { get; set; }
        public int AeronaveId { get; set; }
        public int PilotoId { get; set; }
    }
}
