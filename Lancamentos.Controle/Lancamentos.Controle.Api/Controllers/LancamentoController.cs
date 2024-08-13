using Lancamentos.Controle.Application.Features.Lancamento.Commands;
using Lancamentos.Controle.Application.Infra.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lancamentos.Controle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : BaseController
    {
        private readonly IMediator _mediator;

        public LancamentoController(INotificador notificador, IMediator mediator) : base(notificador)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RealizarLancamentoCommand request)
        {
            return CustomResponse(await _mediator.Send(request));
        }

    }
}
