using Dapper;
using Lancamentos.Controle.Application.Dto;
using Lancamentos.Controle.Application.Features.Lancamento.Commands;
using Lancamentos.Controle.Application.Infra.Repositories;
using Lancamentos.Controle.Infra.Data.Session;
using System.Data;

namespace Lancamentos.Controle.Infra.Data.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
        private readonly DbSession _session;

        public LancamentoRepository(DbSession session)
            => _session = session;
        public async Task<bool> Incluir(RealizarLancamentoCommand command)
        {
            var query = @"INSERT INTO Lancamento(Valor, Tipo, DataLancamento)
                          VALUES(@Valor, @Tipo, @DataLancamento)";

            var parms = new DynamicParameters();

            parms.Add("@Valor", command.Valor);
            parms.Add("@Tipo", command.Tipo.ToUpper());
            parms.Add("@DataLancamento", command.DataLancamento.Date);

            var result =
                await _session
                        .Connection
                        .ExecuteAsync(sql: query,
                                      param: parms,
                                      commandType: CommandType.Text,
                                      transaction: _session.Transaction);
            return result > 0;
        }
    
        public async Task<IEnumerable<ConsolidadoDiarioDto>> BuscarConsolidadoDiario()
        {
            var query = @"SELECT DataLancamento,
                                 SUM(CASE WHEN Tipo = 'CREDITO' THEN Valor 
                                          WHEN Tipo = 'DEBITO' THEN -Valor 
                                          ELSE 0 END) AS SaldoDiario
                          FROM Lancamento
                          GROUP BY DataLancamento
                          ORDER BY DataLancamento";

            var result =
                await _session
                        .Connection
                        .QueryAsync<ConsolidadoDiarioDto>(sql: query,
                                                          commandType: CommandType.Text,
                                                          transaction: _session.Transaction);
            return result;
        }
    
    }
}
