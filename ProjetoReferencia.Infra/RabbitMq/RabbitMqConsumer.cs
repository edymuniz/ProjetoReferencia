using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System.Text;
using ProjetoReferencia.Domain.Entity;
using System.Text.Json;
using ProjetoReferencia.Infra.Data;

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
        CreateConnection();
        return base.StartAsync(cancellationToken);
    }

    private void CreateConnection()
    {
        try
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "NewBikeRegisteredQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
        catch (BrokerUnreachableException ex)
        {
            Console.WriteLine($"Erro ao conectar ao RabbitMQ: {ex.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (_channel == null)
            {
                CreateConnection(); // Tente reconectar se o canal estiver nulo
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);
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

            _channel.BasicConsume(queue: "NewBikeRegisteredQueue", autoAck: true, consumer: consumer);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro em RabbitMqConsumer: {ex.Message}");
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
