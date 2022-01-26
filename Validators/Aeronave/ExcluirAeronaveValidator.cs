using FluentValidation;
using WebApplication3.Contexts;

namespace WebApplication3.Validators.Aeronave
{
    public class ExcluirAeronaveValidator: AbstractValidator<int>
    {
        private readonly CiaAereaContext _context;
        public ExcluirAeronaveValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(id => _context.Aeronaves.Find(id))
                .NotNull().WithMessage("Id da aeronave inválido.")
                .Must(aeronave => aeronave?.Voos!.Count() == 0).WithMessage("Não é possível excluir uma aeronave que já possui voos registrados.")
                .Must(aeronave => aeronave?.Manutencoes!.Count() == 0).WithMessage("Não é possível excluir uma aeronave que já possui manutenções registradas.");
        }
    }
}
