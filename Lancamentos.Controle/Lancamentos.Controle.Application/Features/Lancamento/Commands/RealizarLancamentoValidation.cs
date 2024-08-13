using FluentValidation;

namespace Lancamentos.Controle.Application.Features.Lancamento.Commands
{
    public class RealizarLancamentoValidation : AbstractValidator<RealizarLancamentoCommand>
    {
        public RealizarLancamentoValidation()
        {
            RuleFor(command => command.Valor)
                .GreaterThan(0)
                .WithMessage("Valor deve ser maior que zero.");

            RuleFor(command => command.Tipo)
                .NotNull()
                .WithMessage("Tipo não pode ser nulo.");

            RuleFor(command => command.Tipo)
                .Must(tipo => tipo.ToUpper().Equals("CREDITO") || tipo.ToUpper().Equals("DEBITO"))
                .WithMessage("Tipo deve ser CREDITO ou DEBITO.");

            RuleFor(command => command.DataLancamento.Date)
                .LessThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("Data Lançamento não pode ser uma data futura.");
        }
    }
}
