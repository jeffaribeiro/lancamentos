using MediatR;

namespace Lancamentos.Controle.Application.Features.Lancamento.Events
{
    public class LancamentoRealizadoEvent : INotification
    {
        public Guid EventId { get; private set; }

        public LancamentoRealizadoEvent() 
        {
            EventId = Guid.NewGuid();
        }
    }
}
