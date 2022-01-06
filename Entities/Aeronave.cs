namespace WebApplication3.Entities
{
    public class Aeronave
    {
        public int Id { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string Codigo { get; set; }
        public ICollection<Manutencao> Manutencoes { get; set; }
        public ICollection<Voo> Voos { get; set; }
    }
}
