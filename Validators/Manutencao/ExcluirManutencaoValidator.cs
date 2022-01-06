using FluentValidation;
using WebApplication3.Contexts;

namespace WebApplication3.Validators.Manutencao
{
    public class ExcluirManutencaoValidator : AbstractValidator<int>
    {
        private readonly CiaAereaContext _context;

        public ExcluirManutencaoValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(id => id)
                .NotEmpty().WithMessage("É necessário informar o id da manutenção a ser excluída.")
                .Must(id => _context.Manutencoes.Find(id).DataHora >= DateTime.Now).WithMessage("Não é possível excluir uma manutenção já realizada.");
        }
    }
}
