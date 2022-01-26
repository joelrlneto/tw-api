namespace WebApplication3.Entities
{
    public class Aeronave
    {
        public int Id { get; set; }
        public string Fabricante { get; set; } = String.Empty;
        public string Modelo { get; set; } = String.Empty;
        public string Codigo { get; set; } = String.Empty;
        public ICollection<Manutencao>? Manutencoes { get; set; }
        public ICollection<Voo>? Voos { get; }
    }
}
