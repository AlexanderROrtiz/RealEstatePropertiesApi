using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Properties.Application.Commands.PropertyImageCommand;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Queries.PropertyImage;

namespace RealEstate.Properties.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult> GetByProperty(string propertyId)
        {
            var query = new GetPropertyImagesByPropertyIdQuery(propertyId);
            var result = await _mediator.Send(query);

            if (result == null || !result.Any())
                return Ok(new { message = "No hay imágenes para esta propiedad.", data = new List<PropertyImageDto>() });

            return Ok(new { message = "Imágenes de propiedad obtenidas correctamente.", data = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            // Implementa el query si se necesita
            return NotFound(new { message = "Imagen no encontrada.", data = (PropertyImageDto?)null });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PropertyImageDto dto)
        {
            var result = await _mediator.Send(new CreatePropertyImageCommand(dto));
            if (result == null)
                return BadRequest(new { message = "No se pudo crear la imagen.", data = (PropertyImageDto?)null });
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { message = "Imagen creada correctamente.", data = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] PropertyImageDto dto)
        {
            dto.Id = id;
            var result = await _mediator.Send(new UpdatePropertyImageCommand(dto));
            if (result == null)
                return NotFound(new { message = "Imagen no encontrada.", data = (PropertyImageDto?)null });
            return Ok(new { message = "Imagen actualizada correctamente.", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _mediator.Send(new DeletePropertyImageCommand(id));
            if (!success)
                return NotFound(new { message = "Imagen no encontrada o no se pudo eliminar." });
            return Ok(new { message = "Imagen eliminada correctamente." });
        }
    }
}