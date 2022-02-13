using FluentValidation;
using WebApplication3.Contexts;
using WebApplication3.Entities;
using WebApplication3.Validators.Manutencao;
using WebApplication3.ViewModels.Manutencao;

namespace WebApplication3.Services
{
    public class ManutencaoService
    {
        private readonly CiaAereaContext _context;
        private readonly AdicionarManutencaoValidator _adicionarManutencaoValidator;
        private readonly AtualizarManutencaoValidator _atualizarManutencaoValidator;
        private readonly ExcluirManutencaoValidator _excluirManutencaoValidator;
        public ManutencaoService(CiaAereaContext context, AdicionarManutencaoValidator adicionarManutencaoValidator, AtualizarManutencaoValidator atualizarManutencaoValidator, ExcluirManutencaoValidator excluirManutencaoValidator)
        {
            _context = context;
            _adicionarManutencaoValidator = adicionarManutencaoValidator;
            _atualizarManutencaoValidator = atualizarManutencaoValidator;
            _excluirManutencaoValidator = excluirManutencaoValidator;
        }

        public ListarManutencaoViewModel AdicionarManutencao(AdicionarManutencaoViewModel dados)
        {
            _adicionarManutencaoValidator.ValidateAndThrow(dados);

            var manutencao = new Manutencao
            {
                AeronaveId = dados.AeronaveId,
                DataHora = dados.DataHora,
                Observacoes = dados.Observacoes,
                Tipo = dados.Tipo
            };

            _context.Add(manutencao);
            _context.SaveChanges();

            return new ListarManutencaoViewModel
            {
                AeronaveId = manutencao.AeronaveId,
                DataHora = manutencao.DataHora,
                Id = manutencao.Id,
                Observacoes = manutencao.Observacoes,
                Tipo = manutencao.Tipo
            };
        }

        public void AtualizarManutencao(AtualizarManutencaoViewModel dados)
        {
            _atualizarManutencaoValidator.ValidateAndThrow(dados);

            var manutencao = _context.Manutencoes.Find(dados.Id);
            
            if(manutencao != null)
            {
                manutencao.DataHora = dados.DataHora;
                manutencao.Tipo = dados.Tipo;
                manutencao.Observacoes = dados.Observacoes;
                manutencao.AeronaveId = dados.AeronaveId;

                _context.Update(manutencao);
                _context.SaveChanges();
            }
        }

        public void ExcluirManutencao(int id)
        {
            _excluirManutencaoValidator.ValidateAndThrow(id);

            var manutencao = _context.Manutencoes.Find(id);

            if(manutencao != null)
            {
                _context.Remove(manutencao);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ListarManutencaoViewModel> ListarManutencoes()
        {
            return _context.Manutencoes.Select(m => new ListarManutencaoViewModel
            {
                Id = m.Id,
                DataHora = m.DataHora,  
                Tipo = m.Tipo,  
                Observacoes = m.Observacoes,
            });
        }
    }
}
