using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contexts;
using WebApplication3.ViewModels.Voo;

namespace WebApplication3.Validators.Voo
{
    public class AtualizarVooValidator : AbstractValidator<AtualizarVooViewModel>
    {
        private readonly CiaAereaContext _context;
        public AtualizarVooValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(v => new { v.DataHoraPartida, v.DataHoraChegada })
                 .Must(datas => datas.DataHoraPartida > DateTime.Now).WithMessage("A data/hora de partida do voo deve ser futura.")
                 .Must(datas => datas.DataHoraChegada > datas.DataHoraPartida).WithMessage("A data/hora de chegada do voo deve ser maior que a data/hora de partida.");

            RuleFor(v => v.Origem)
                .NotEmpty().WithMessage("É necessário informar o aeroporto de origem.")
                .Length(3).WithMessage("Aeroporto de origem inválido.");

            RuleFor(v => v.Destino)
                .NotEmpty().WithMessage("É necessário informar o aeroporto de destino.")
                .Length(3).WithMessage("Aeroporto de destino inválido.");

            RuleFor(v => v).Custom((voo, context) =>
            {
                var piloto = _context.Pilotos
                                     .Where(p => p.Id == voo.PilotoId)
                                     .Include(p => p.Voos)
                                     .FirstOrDefault();

                if (piloto == null)
                    context.AddFailure("Piloto inválido.");
                else
                {
                    var emVoo = piloto.Voos.Any(v => v.Id != voo.Id &&
                                                     (v.DataHoraPartida <= voo.DataHoraPartida && v.DataHoraChegada >= voo.DataHoraChegada) ||
                                                     (v.DataHoraPartida >= voo.DataHoraPartida && v.DataHoraPartida <= voo.DataHoraChegada) ||
                                                     (v.DataHoraChegada >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada));

                    if (emVoo)
                        context.AddFailure("Este piloto estará em voo no horário selecionado.");
                }

                var aeronave = _context.Aeronaves
                                       .Where(a => a.Id == voo.AeronaveId)
                                       .Include(a => a.Manutencoes)
                                       .Include(a => a.Voos)
                                       .FirstOrDefault();

                if (aeronave == null)
                    context.AddFailure("Aeronave inválida.");
                else
                {
                    var emManutencao = aeronave.Manutencoes.Any(m => m.DataHora >= voo.DataHoraPartida && m.DataHora <= voo.DataHoraChegada);

                    if (emManutencao)
                        context.AddFailure("Esta aeronave estará em manutenção no horário selecionado.");

                    var emVoo = aeronave.Voos.Any(v => v.Id == voo.Id &&
                                                       (v.DataHoraPartida <= voo.DataHoraPartida && v.DataHoraChegada >= voo.DataHoraChegada) ||
                                                       (v.DataHoraPartida >= voo.DataHoraPartida && v.DataHoraPartida <= voo.DataHoraChegada) ||
                                                       (v.DataHoraChegada >= voo.DataHoraPartida && v.DataHoraChegada <= voo.DataHoraChegada));

                    if (emVoo)
                        context.AddFailure("Esta aeronave estará em voo no horário selecionado.");
                }
            });
        }
    }
}
