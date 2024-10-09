using Moq;
using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Domain.Interfaces.Repositories;
using ProjetoReferencia.Domain.Interfaces.Services;
using ProjetoReferencia.Domain.Services;
using ProjetoReferencia.Domain.ExternalServices.RabbitMq;
using AutoMapper;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoReferencia.Domain.DTO.Bike.Response;

namespace ProjetoReferencia.Tests
{
    public class BikeServiceTests
    {
        private readonly Mock<IBikeRepository> _bikeRepositoryMock;
        private readonly Mock<IRabbitMqProducer> _rabbitMqProducerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IBikeService _bikeService;

        public BikeServiceTests()
        {
            _bikeRepositoryMock = new Mock<IBikeRepository>();
            _rabbitMqProducerMock = new Mock<IRabbitMqProducer>();
            _mapperMock = new Mock<IMapper>();

            _bikeService = new BikeService(_bikeRepositoryMock.Object, _rabbitMqProducerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetBikes_ShouldReturnAllBikes()
        {
            // Arrange
            var bikes = new List<Bike>
            {
                new Bike { Id = 1, Identifier = "BK-001", Year = 2020, Model = "Honda CB 300", Plate = "ABC1234" },
                new Bike { Id = 2, Identifier = "BK-002", Year = 2021, Model = "Yamaha Fazer", Plate = "XYZ5678" }
            };

            var bikeResponseDtos = new List<BikeResponseDto>
            {
                new BikeResponseDto { Identifier = "BK-001", Year = 2020, Model = "Honda CB 300", Plate = "ABC1234" },
                new BikeResponseDto { Identifier = "BK-002", Year = 2021, Model = "Yamaha Fazer", Plate = "XYZ5678" }
            };

            _bikeRepositoryMock.Setup(repo => repo.GetAllBikesAsync()).ReturnsAsync(bikes);
            _mapperMock.Setup(m => m.Map<List<BikeResponseDto>>(bikes)).Returns(bikeResponseDtos);

            // Act
            var result = await _bikeService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
