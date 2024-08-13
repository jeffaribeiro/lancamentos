using Lancamentos.Controle.Application.Infra.Repositories;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Lancamentos.Controle.Application.Features.Lancamento.Events
{
    public class LancamentoRealizadoEventHandler : INotificationHandler<LancamentoRealizadoEvent>
    {
        private readonly ILancamentoRepository _lancamentoRepository;

        public LancamentoRealizadoEventHandler(ILancamentoRepository lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        public async Task Handle(LancamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "lancamentos.rabbitmq" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consolidadoDiario = await _lancamentoRepository.BuscarConsolidadoDiario();
            var message = JsonSerializer.Serialize(consolidadoDiario);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "lancamentos", basicProperties: null, body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}
