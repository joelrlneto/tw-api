using FluentValidation;
using WebApplication3.Contexts;
using WebApplication3.Entities;
using WebApplication3.Validators.Aeronave;
using WebApplication3.ViewModels.Aeronave;

namespace WebApplication3.Services
{
    public class AeronaveService
    {
        private readonly CiaAereaContext _context;
        private readonly AdicionarAeronaveValidator _adicionarAeronaveValidator;
        private readonly AtualizarAeronaveValidator _atualizarAeronaveValidator;
        private readonly ExcluirAeronaveValidator _excluirAeronaveValidator;
        public AeronaveService(CiaAereaContext context, AdicionarAeronaveValidator adicionarAeronaveValidator, AtualizarAeronaveValidator atualizarAeronaveValidator, ExcluirAeronaveValidator excluirAeronaveValidator)
        {
            _context = context;
            _adicionarAeronaveValidator = adicionarAeronaveValidator;
            _atualizarAeronaveValidator = atualizarAeronaveValidator;
            _excluirAeronaveValidator = excluirAeronaveValidator;
        }

        public DetalhesAeronaveViewModel AdicionarAeronave(AdicionarAeronaveViewModel dados)
        {
            _adicionarAeronaveValidator.ValidateAndThrow(dados);

            var aeronave = new Aeronave
            {
                Fabricante = dados.Fabricante,
                Modelo = dados.Modelo,
                Codigo = dados.Codigo
            };

            _context.Add(aeronave);
            _context.SaveChanges();

            return new DetalhesAeronaveViewModel
            {
                Id = aeronave.Id,
                Modelo = aeronave.Modelo,
                Fabricante = aeronave.Fabricante,
                Codigo = aeronave.Codigo
            };
        }

        public DetalhesAeronaveViewModel? AtualizarAeronave(AtualizarAeronaveViewModel dados)
        {
            _atualizarAeronaveValidator.ValidateAndThrow(dados);

            var aeronave = _context.Aeronaves.Find(dados.Id);

            if(aeronave != null)
            {
                aeronave.Fabricante = dados.Fabricante;
                aeronave.Modelo = dados.Modelo;
                aeronave.Codigo = dados.Codigo;

                _context.Update(aeronave);
                _context.SaveChanges();

                return new DetalhesAeronaveViewModel
                {
                    Id = aeronave.Id,
                    Modelo = aeronave.Modelo,
                    Fabricante = aeronave.Fabricante,
                    Codigo = aeronave.Codigo
                };
            }
            else
                return null;
        }

        public void ExcluirAeronave(int id)
        {
            _excluirAeronaveValidator.ValidateAndThrow(id);

            var aeronave = _context.Aeronaves.Find(id);

            if (aeronave != null)
            {
                _context.Remove<Aeronave>(aeronave);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ListarAeronaveViewModel> ListarAeronaves()
        {
            return _context.Aeronaves.Select(a => new ListarAeronaveViewModel
            {
                Id = a.Id,
                Codigo = a.Codigo,
                Modelo = a.Modelo
            });
        }

        public DetalhesAeronaveViewModel? ListarAeronavePeloId(int id)
        {
            var aeronave = _context.Aeronaves.Find(id);

            if (aeronave != null)
            {
                return new DetalhesAeronaveViewModel
                {
                    Id = aeronave.Id,
                    Fabricante = aeronave.Fabricante,
                    Codigo = aeronave.Codigo,
                    Modelo = aeronave.Modelo
                };
            }
            else
                return null;
        }
    }
}
