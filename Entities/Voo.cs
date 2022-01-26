namespace WebApplication3.Entities
{
    public class Voo
    {
        public int Id { get; set; }
        public string Origem { get; set; } = String.Empty;
        public string Destino { get; set; } = String.Empty;
        public DateTime DataHoraPartida { get; set; }
        public DateTime DataHoraChegada { get; set; }
        public int AeronaveId { get; set; }
        public int PilotoId { get; set; }
        public Aeronave? Aeronave { get; set; }
        public Piloto? Piloto { get; set; }
        public Cancelamento? Cancelamento { get; set; }
    }
}
