using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Domain.ExternalServices.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProjetoReferencia.Infra.RabbitMq
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMqProducer(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task PublishAsync(Bike bike)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "NewBikeRegisteredQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            var message = JsonSerializer.Serialize(bike);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "NewBikeRegisteredQueue", basicProperties: properties, body: body);
        }
    }
}
