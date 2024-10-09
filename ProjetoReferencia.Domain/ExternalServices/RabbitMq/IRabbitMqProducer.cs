using ProjetoReferencia.Domain.Entity;

namespace ProjetoReferencia.Domain.ExternalServices.RabbitMq
{
    public interface IRabbitMqProducer
    {
        Task PublishAsync(Bike bike);
    }
}
