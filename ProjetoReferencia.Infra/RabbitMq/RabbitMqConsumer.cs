using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Infra.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ProjetoReferencia.Infra.RabbitMq
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqConsumer(IConnectionFactory connectionFactory, IServiceScopeFactory scopeFactory)
        {
            _connectionFactory = connectionFactory;
            _scopeFactory = scopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "BikeRegisteredQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(_channel); // Alterado para AsyncEventingBasicConsumer

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var bike = JsonSerializer.Deserialize<Bike>(message);

                    if (bike?.Year == 2024)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                            dbContext.Bike.Add(bike);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                };

                _channel.BasicConsume(queue: "BikeRegisteredQueue", autoAck: true, consumer: consumer);
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                // Logue a exceção para depuração
                Console.WriteLine($"Error in RabbitMqConsumer: {ex.Message}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _connection?.Close();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
