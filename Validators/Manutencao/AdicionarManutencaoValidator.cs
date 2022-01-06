using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contexts;
using WebApplication3.ViewModels.Manutencao;

namespace WebApplication3.Validators.Manutencao
{
    public class AdicionarManutencaoValidator : AbstractValidator<AdicionarManutencaoViewModel>
    {
        private readonly CiaAereaContext _context;
        public AdicionarManutencaoValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(m => m.DataHora)
                .Must(data => data >= DateTime.Now).WithMessage("A data/hora da manutenção deve ser futura.");

            RuleFor(m => m.Tipo)
                .NotNull().WithMessage("É necessário informar o tipo da manutenção.");

            RuleFor(m => m)
                .Custom((manutencao, context) =>
                {
                    var aeronave = _context.Aeronaves.Where(a => a.Id == manutencao.AeronaveId).Include(a => a.Voos).FirstOrDefault();

                    if (aeronave == null)
                        context.AddFailure("Aeronave inválida.");
                    else
                    {
                        var emVoo = aeronave.Voos.Any(v => manutencao.DataHora >= v.DataHoraPartida && manutencao.DataHora <= v.DataHoraChegada);

                        if (emVoo)
                            context.AddFailure("Esta aeronave estará em voo no horário selecionado.");
                    }
                });
        }
    }
}
