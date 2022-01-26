namespace WebApplication3.ViewModels.Voo
{
    public class ListarVooViewModel
    {
        public int Id { get; set; }
        public string Origem { get; set; } = String.Empty;
        public string Destino { get; set; } = String.Empty;
        public DateTime DataHoraPartida { get; set; }
        public DateTime DataHoraChegada { get; set; }
    }
}
