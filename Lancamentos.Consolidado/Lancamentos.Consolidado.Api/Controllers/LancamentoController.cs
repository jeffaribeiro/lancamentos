using Lancamentos.Consolidado.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace Lancamentos.Consolidado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public LancamentoController(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        [HttpGet("consolidado-diario")]
        public async Task<IActionResult> Get([FromQuery] DateTime dataLancamento)
        {
            var db = _redisConnection.GetDatabase();

            var response = await db.StringGetAsync(dataLancamento.Date.ToString());

            if (response.HasValue)
                return Ok(new { success = true, message = JsonSerializer.Deserialize<ConsolidadoDiarioDto>(response) });
            else
                return BadRequest(new { success = false, message = $"Não existem lançamentos para {dataLancamento.Date}" });
        }
    }
}
