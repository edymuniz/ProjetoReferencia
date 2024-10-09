using AutoMapper;
using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;
using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Domain.ExternalServices.RabbitMq;
using ProjetoReferencia.Domain.Interfaces.Repositories;
using ProjetoReferencia.Domain.Interfaces.Services;
using ProjetoReferencia.Domain.Mapper;

namespace ProjetoReferencia.Domain.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly IMapper _mapper;

        public BikeService(IBikeRepository bikeRepository, IRabbitMqProducer rabbitMqProducer, IMapper mapper)
        {
            _bikeRepository = bikeRepository;
            _rabbitMqProducer = rabbitMqProducer;
            _mapper = mapper;
        }

        public async Task<BikeResponseDto> GetByIdAsync(int id)
        {
            var bike = await _bikeRepository.GetByIdAsync(id);
            if (bike == null)
                return null;

            return bike.ToBikeResponseDto();
        }

        public async Task<BikeResponseDto> UpdateBikeAsync(int id, string newPlate)
        {
            var updatedBike = await _bikeRepository.UpdatePlateAsync(id, newPlate);
            if (updatedBike == null)
            {
                throw new Exception("Moto não encontrada ou erro ao atualizar a placa.");
            }

            return _mapper.Map<BikeResponseDto>(updatedBike);
        }

        public async Task<BikeResponseDto> RegisterBikeAsync(BikeRequestDto bikeRequest)
        {
            if (!await _bikeRepository.IsPlateUniqueAsync(bikeRequest.Plate))
            {
                throw new Exception("A placa já está cadastrada.");
            }

            var bike = _mapper.Map<Bike>(bikeRequest);
            var newBike = await _bikeRepository.AddAsync(bike);
            if (newBike == null)
                return null;

            await _rabbitMqProducer.PublishAsync(newBike);
            _mapper.Map<BikeResponseDto>(newBike);

            return _mapper.Map<BikeResponseDto>(newBike);
        }

        public async Task<IEnumerable<BikeResponseDto>> GetBikesAsync(string? plate)
        {
            var listBike = await _bikeRepository.GetBikesByPlateAsync(plate);
            if (listBike == null)
                return null;

            return _mapper.Map<List<BikeResponseDto>>(listBike);
        }

        public async Task<IEnumerable<BikeResponseDto>> GetAllAsync()
        {
            var listBike = await _bikeRepository.GetAllBikesAsync();
            if (listBike == null)
                return null;

            return _mapper.Map<List<BikeResponseDto>>(listBike);
        }

        public async Task<string> DeleteAsync(int id)
        {

            return await _bikeRepository.DeleteAsync(id);
        }
    }
}
