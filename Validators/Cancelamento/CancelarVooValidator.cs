using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contexts;
using WebApplication3.ViewModels.Cancelamento;

namespace WebApplication3.Validators.Cancelamento
{
    public class CancelarVooValidator : AbstractValidator<CancelarVooViewModel>
    {
        private readonly CiaAereaContext _context;
        
        public CancelarVooValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((cancelamento, context) =>
            {
                var voo = _context.Voos.Include(v => v.Cancelamento).Where(v => v.Id == cancelamento.VooId).FirstOrDefault();

                if (voo == null)
                    context.AddFailure("Voo inválido.");
                else
                {
                    if (voo.Cancelamento != null)
                        context.AddFailure("Não é possível cancelar um voo que já foi cancelado.");

                    if (voo.DataHoraPartida <= DateTime.Now && voo.DataHoraChegada >= DateTime.Now)
                        context.AddFailure("Não é possível cancelar um voo em andamento.");
                }
            });                
        }
    }
}
