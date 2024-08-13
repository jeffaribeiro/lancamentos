using MediatR;

namespace Lancamentos.Controle.Application.Features.Lancamento.Commands
{
    public class RealizarLancamentoCommand : IRequest<bool>
    {
        public decimal Valor {  get; set; }
        public string Tipo {  get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
