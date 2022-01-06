namespace WebApplication3.Entities
{
    public class Piloto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public ICollection<Voo> Voos { get; set; }
    }
}
