using Microsoft.AspNetCore.Mvc;
using ProjetoReferencia.Application.Interface.Bike;
using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;
using ProjetoReferencia.Domain.Entity;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoReferencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikesController : ControllerBase
    {
        private readonly IBikeAppService _bikeAppService;

        public BikesController(IBikeAppService bikeAppService)
        {
            _bikeAppService = bikeAppService;
        }

        [SwaggerOperation(Summary = "Create a new bike.")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Bike created successfully", typeof(Bike))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BikeRequestDto bikeRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registeredBike = await _bikeAppService.PostAsync(bikeRequestDto);
            if (registeredBike == null)
            {
                return BadRequest("Não foi possível cadastrar a moto!");
            }

            // Retorna 201 Created, com o recurso criado e a rota para obtê-lo
            return CreatedAtAction(nameof(GetById), new { id = registeredBike.Id }, registeredBike);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all bikes or filter by plate.")]
        public async Task<IActionResult> GetBikes([FromQuery] string? plate = null)
        {
            var result = await _bikeAppService.GetBikesAsync(plate);
            return Ok(result);
        }

        [HttpGet("all")] 
        [SwaggerOperation(Summary = "Get all bikes without filtering.")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bikeAppService.GetAllAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update bike plate by ID")]
        [SwaggerResponse(200, "Placa atualizada com sucesso.", typeof(BikeResponseDto))] 
        [SwaggerResponse(400, "Requisição inválida. Erro na validação ou placa duplicada.", typeof(string))]  
        [SwaggerResponse(404, "Moto não encontrada com o ID fornecido.", typeof(string))] 
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> UpdatePlate(int id, [FromBody] string newPlate)
        {
            try
            {
                var result = await _bikeAppService.PutAsync(id, newPlate);
                return Ok(result); 
            }
            catch (KeyNotFoundException) 
            {
                return NotFound("Moto não encontrada."); // Retorna 404 NotFound
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get bike by ID.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _bikeAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete a bike by ID")]
        [SwaggerResponse(200, "Moto deletada com sucesso.", typeof(string))]
        [SwaggerResponse(404, "Moto não encontrada com o ID fornecido.", typeof(string))]
        [SwaggerResponse(500, "Erro ao remover moto.", typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bikeAppService.DeleteAsync(id);

            if (result == "Moto não encontrada.")
            {
                return NotFound(result);
            }

            if (result.StartsWith("Erro ao remover moto"))
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}
