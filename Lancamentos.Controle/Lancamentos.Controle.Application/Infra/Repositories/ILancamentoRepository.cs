using Lancamentos.Controle.Application.Dto;
using Lancamentos.Controle.Application.Features.Lancamento.Commands;

namespace Lancamentos.Controle.Application.Infra.Repositories
{
    public interface ILancamentoRepository
    {
        Task<bool> Incluir(RealizarLancamentoCommand command);
        Task<IEnumerable<ConsolidadoDiarioDto>> BuscarConsolidadoDiario();
    }
}
