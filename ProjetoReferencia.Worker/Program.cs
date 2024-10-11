using Microsoft.EntityFrameworkCore;
using ProjetoReferencia.Infra.RabbitMq;
using ProjetoReferencia.Infra.Data;
using RabbitMQ.Client;

namespace ProjetoReferencia.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Configurando RabbitMQ
                    var rabbitMqSettings = new RabbitMqSettings
                    {
                        Host = "rabbitmq_server",
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest",
                        VirtualHost = "/"
                    };

                    services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
                    {
                        HostName = rabbitMqSettings.Host,
                        Port = rabbitMqSettings.Port,
                        UserName = rabbitMqSettings.UserName,
                        Password = rabbitMqSettings.Password,
                        VirtualHost = rabbitMqSettings.VirtualHost,
                        DispatchConsumersAsync = true
                    });

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql("Host=postgres;Port=5432;Username=admin;Password=admin123;Database=moto_rental"));

                    // Ativa o RabbitMqConsumer com base na configuração
                    bool enableRabbitMqConsumer = context.Configuration.GetValue<bool>("ENABLE_RABBITMQ_CONSUMER");
                    if (enableRabbitMqConsumer)
                    {
                        services.AddHostedService<RabbitMqConsumer>(); 
                    }
                    services.AddHostedService<Worker>(); 
                })
                .Build();

            host.Run();
        }
    }


}
