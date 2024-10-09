using Microsoft.AspNetCore.Mvc;
using ProjetoReferencia.Application.Interface.Rental;
using ProjetoReferencia.Domain.Constantes;
using ProjetoReferencia.Domain.DTO.Rental.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoReferencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : Controller
    {

        private readonly IRentalAppService _rentalAppService;
        public RentalController(IRentalAppService  rentalAppService)
        {
            _rentalAppService = rentalAppService;
        }        

        [HttpPost]
        [SwaggerOperation(Summary = DescriptionSwagger.DescriptionRental.PostTag)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IActionResult> Create([FromBody] RentalRequestDto rentalRequestDto)
        {
            var result = _rentalAppService.PostAsync(rentalRequestDto);
            if (result == null)
                return NotFound();
            return StatusCode(201, rentalRequestDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = DescriptionSwagger.DescriptionRental.GetByIdTag)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IActionResult> GetByIdl([FromRoute] int id)
        {
            var result = _rentalAppService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}/devolucao")]
        [SwaggerOperation(Summary = DescriptionSwagger.DescriptionRental.PutTag)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RentalDateReturnRequestDto rentalDateReturnRequestDto)
        {
            var result = _rentalAppService.PutAsync(id, rentalDateReturnRequestDto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

    }
}
