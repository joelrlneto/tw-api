using FluentValidation;
using WebApplication3.Contexts;
using WebApplication3.Entities;
using WebApplication3.Validators.Piloto;
using WebApplication3.ViewModels.Piloto;

namespace WebApplication3.Services
{
    public class PilotoService
    {
        private readonly CiaAereaContext _context;
        private readonly AdicionarPilotoValidator _adicionarPilotoValidator;
        private readonly AtualizarPilotoValidator _atualizarPilotoValidator;
        private readonly ExcluirPilotoValidator _excluirPilotoValidator;
        public PilotoService(CiaAereaContext context, AdicionarPilotoValidator adicionarPilotoValidator, AtualizarPilotoValidator atualizarPilotoValidator, ExcluirPilotoValidator excluirPilotoValidator)
        {
            _context = context;
            _adicionarPilotoValidator = adicionarPilotoValidator;
            _atualizarPilotoValidator = atualizarPilotoValidator;
            _excluirPilotoValidator = excluirPilotoValidator;
        }

        public DetalhesPilotoViewModel AdicionarPiloto(AdicionarPilotoViewModel dados)
        {
            _adicionarPilotoValidator.ValidateAndThrow(dados);

            var piloto = new Piloto
            {
                Matricula = dados.Matricula,
                Nome = dados.Nome
            };

            _context.Add(piloto);
            _context.SaveChanges();

            return new DetalhesPilotoViewModel
            {
                Id = piloto.Id,
                Nome = piloto.Nome,
                Matricula = piloto.Matricula
            };
        }

        public DetalhesPilotoViewModel? AtualizarPiloto(AtualizarPilotoViewModel dados)
        {
            _atualizarPilotoValidator.ValidateAndThrow(dados);

            var piloto = _context.Pilotos.Find(dados.Id);

            if(piloto != null)
            {
                piloto.Matricula = dados.Matricula;
                piloto.Nome = dados.Nome;

                _context.Update(piloto);
                _context.SaveChanges();

                return new DetalhesPilotoViewModel
                {
                    Id = piloto.Id,
                    Nome = piloto.Nome,
                    Matricula = piloto.Matricula
                };
            }
            
            return null;
        }

        public void ExcluirPiloto(int id)
        {
            _excluirPilotoValidator.ValidateAndThrow(id);

            var piloto = _context.Pilotos.Find(id);
            
            if(piloto != null)
            {
                _context.Remove<Piloto>(piloto);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ListarPilotoViewModel> ListarPilotos()
        {
            return _context.Pilotos.Select(p => new ListarPilotoViewModel
            {
                Id = p.Id,
                Nome = p.Nome
            });
        }

        public DetalhesPilotoViewModel? ListarPilotoPeloId(int id)
        {
            var piloto = _context.Pilotos.Find(id);

            if (piloto != null)
            {
                return new DetalhesPilotoViewModel
                {
                    Id = piloto.Id,
                    Matricula = piloto.Matricula,
                    Nome = piloto.Nome
                };
            }
            
            return null;
        }
    }
}
