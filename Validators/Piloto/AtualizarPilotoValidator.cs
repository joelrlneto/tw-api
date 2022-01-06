using FluentValidation;
using WebApplication3.Contexts;
using WebApplication3.ViewModels.Piloto;

namespace WebApplication3.Validators.Piloto
{
    public class AtualizarPilotoValidator : AbstractValidator<AtualizarPilotoViewModel>
    {
        private readonly CiaAereaContext _context;

        public AtualizarPilotoValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(p => p.Matricula)
                .NotEmpty().WithMessage("É necessário informar a matrícula do piloto.")
                .MaximumLength(10).WithMessage("A matrícula do piloto deve ter no máximo 10 caracteres.");

            RuleFor(p => p)
                .Must(piloto => !_context.Pilotos.Any(p => p.Matricula == piloto.Matricula && p.Id != piloto.Id)).WithMessage("Já existe um piloto com essa matrícula.");

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("É necessário informar o nome do piloto.")
                .MaximumLength(100).WithMessage("O nome do piloto deve ter no máximo 100 caracteres.");
        }
    }
}
