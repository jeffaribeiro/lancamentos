using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Lancamentos.Consolidado.Api.Dto;
using StackExchange.Redis;

namespace Lancamentos.Consolidado.Api
{
    public class QueueConsumer : BackgroundService
    {
        private readonly ILogger _logger;

        public QueueConsumer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueueConsumer>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var intervalo = TimeSpan.FromSeconds(10);

            _logger.LogInformation("Background service iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory() { HostName = "lancamentos.rabbitmq" };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(" [x] Received {0}", message);

                    var redis = ConnectionMultiplexer.Connect("lancamentos.redis");
                    var db = redis.GetDatabase();

                    var consolidadoDiario = JsonSerializer.Deserialize<IEnumerable<ConsolidadoDiarioDto>>(message);

                    foreach (var item in consolidadoDiario)
                    {
                        var key = item.DataLancamento.Date.ToString();
                        var value = JsonSerializer.Serialize(item);

                        db.StringSet(key, value);

                        _logger.LogInformation($"Chave: {key} | Valor: {value}");
                    }
                };

                channel.BasicConsume(queue: "lancamentos", autoAck: true, consumer: consumer);

                await Task.Delay(intervalo, stoppingToken);
            }
        }
    }
}
