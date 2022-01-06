using FluentValidation;
using WebApplication3.Contexts;

namespace WebApplication3.Validators.Piloto
{
    public class ExcluirPilotoValidator : AbstractValidator<int>
    {
        private readonly CiaAereaContext _context;
        public ExcluirPilotoValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(id => _context.Pilotos.Find(id))
                .NotNull().WithMessage("Id do piloto inválido.")
                .Must(piloto => piloto?.Voos.Count() == 0).WithMessage("Não é possível excluir um piloto que já possui voos registrados.");
        }
    }
}
