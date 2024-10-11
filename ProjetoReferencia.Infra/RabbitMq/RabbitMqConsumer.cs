using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using ProjetoReferencia.Domain.Entity;
using System.Text.Json;
using ProjetoReferencia.Infra.Data;
using ProjetoReferencia.Domain.DTO.Bike.Request;

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
        _channel.QueueDeclare(queue: "NewBikeRegisteredQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var bike = JsonSerializer.Deserialize<Bike>(message);

            ///
            /// Manutenção do código: É uma boa prática manter a lógica do consumidor de mensagens separada 
            /// do restante do código para facilitar a manutenção e o entendimento.
            ///
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

        _channel.BasicConsume(queue: "NewBikeRegisteredQueue", autoAck: true, consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
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
