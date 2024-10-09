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
                        Host = Environment.GetEnvironmentVariable("RABBITMQ__Host") ?? "rabbitmq", // Mude para "rabbitmq"
                        Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ__Port") ?? "5672"),
                        UserName = Environment.GetEnvironmentVariable("RABBITMQ__UserName") ?? "guest",
                        Password = Environment.GetEnvironmentVariable("RABBITMQ__Password") ?? "guest",
                        VirtualHost = Environment.GetEnvironmentVariable("RABBITMQ__VirtualHost") ?? "/"
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

                    // Configurando o DbContext do PostgreSQL
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql("Host=postgres;Port=5432;Username=admin;Password=admin123;Database=moto_rental")); // Utilize o nome do serviço postgres

                    // Registrando os serviços
                    services.AddHostedService<RabbitMqConsumer>();
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
    
}
