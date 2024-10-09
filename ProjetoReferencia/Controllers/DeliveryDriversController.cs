using Microsoft.AspNetCore.Mvc;
using ProjetoReferencia.Application.Interface.DeliveryDrivers;
using ProjetoReferencia.Domain.Constantes;
using ProjetoReferencia.Domain.DTO.DeliveryDrivers.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoReferencia.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryDriversController : Controller
    {
        private readonly IDeliveryDrivesAppService _deliveryDrivesAppService;
        
        public DeliveryDriversController(IDeliveryDrivesAppService deliveryDrivesAppService)
        {
            _deliveryDrivesAppService = deliveryDrivesAppService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = DescriptionSwagger.DescriptionDeliveryDriver.PostTag)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IActionResult> Create([FromBody] DeliveryDriverRequestDto deliveryDriverRequestDto)
        {
            var result = _deliveryDrivesAppService.PostAsync(deliveryDriverRequestDto);
            if (result == null)
                return NotFound();
            return StatusCode(201, deliveryDriverRequestDto);
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> UploadDriverImage([FromRoute] int id, [FromBody] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Image file is invalid.");
            }
 
            var imageUrl = await _deliveryDrivesAppService.SaveFileAsync(imageFile);

            return Ok(new { ImageUrl = imageUrl });
        }
    }
}
