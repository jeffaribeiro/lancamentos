using Lancamentos.Controle.Application.Base;
using Lancamentos.Controle.Application.Features.Lancamento.Events;
using Lancamentos.Controle.Application.Infra.Notification;
using Lancamentos.Controle.Application.Infra.Repositories;
using MediatR;

namespace Lancamentos.Controle.Application.Features.Lancamento.Commands
{
    public class RealizarLancamentoCommandHandler : BaseHandler, IRequestHandler<RealizarLancamentoCommand, bool>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IMediator _mediator;

        public RealizarLancamentoCommandHandler(ILancamentoRepository lancamentoRepository, IMediator mediator, INotificador notificador) : base(notificador)
        {
            _lancamentoRepository = lancamentoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(RealizarLancamentoCommand request, CancellationToken cancellationToken)
        {
            if (!ExecutarValidacao(new RealizarLancamentoValidation(), request)) return false;

            var sucesso = await _lancamentoRepository.Incluir(request);

            if (sucesso)
                await _mediator.Publish(new LancamentoRealizadoEvent());

            return sucesso;
        }
    }
}
