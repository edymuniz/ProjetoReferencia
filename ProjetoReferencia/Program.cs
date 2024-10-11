using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjetoReferencia.Application.Interface.Bike;
using ProjetoReferencia.Application.Interface.DeliveryDrivers;
using ProjetoReferencia.Application.Interface.Rental;
using ProjetoReferencia.Domain.ExternalServices.AWS;
using ProjetoReferencia.Domain.ExternalServices.RabbitMq;
using ProjetoReferencia.Domain.Interfaces.Repositories;
using ProjetoReferencia.Domain.Interfaces.Services;
using ProjetoReferencia.Domain.Services;
using ProjetoReferencia.Infra.Data;
using ProjetoReferencia.Infra.RabbitMq;
using ProjetoReferencia.Infra.Repository;
using RabbitMQ.Client;

namespace ProjetoReferencia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do logger
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole(); // ou AddDebug, conforme necessário

            // Configuração de banco de dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configurar RabbitMQ
            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
            builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                Port = rabbitMqSettings.Port,
                UserName = rabbitMqSettings.UserName,
                Password = rabbitMqSettings.Password,
                VirtualHost = rabbitMqSettings.VirtualHost,
                DispatchConsumersAsync = true
            });


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adicionar serviços de aplicação
            builder.Services.AddScoped<IBikeAppService, BikeAppService>();
            builder.Services.AddScoped<IDeliveryDrivesAppService, DeliveryDrivesAppService>();
            builder.Services.AddScoped<IRentalAppService, RentalAppService>();
            builder.Services.AddScoped<IBikeService, BikeService>();
            builder.Services.AddScoped<IStorageService, S3StorageService>();
            builder.Services.AddScoped<IBikeRepository, BikeRepository>();
            builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

            // Adicionar controllers
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Configurar Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger"; // Defina o prefixo desejado
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
