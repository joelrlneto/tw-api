namespace WebApplication3.Entities
{
    public class Piloto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Matricula { get; set; } = String.Empty;
        public ICollection<Voo>? Voos { get; }
    }
}
