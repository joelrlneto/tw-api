namespace WebApplication3.Entities
{
    public class Cancelamento
    {
        public int Id { get; set; }
        public DateTime DataHoraNotificacao { get; set; }
        public string Motivo { get; set; }
        public int VooId { get; set; }
        public Voo Voo { get; set; }
    }
}
