using FluentValidation;
using WebApplication3.Contexts;

namespace WebApplication3.Validators.Voo
{
    public class ExcluirVooValidator : AbstractValidator<int>
    {
        private readonly CiaAereaContext _context;

        public ExcluirVooValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(id => _context.Voos.Find(id))
                .Must(voo => voo.DataHoraChegada >= DateTime.Now).WithMessage("Não é possível excluir um voo que já ocorreu.");
        }
    }
}
