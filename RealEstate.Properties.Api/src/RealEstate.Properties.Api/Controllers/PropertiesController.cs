using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Properties.Application.Commands.PropertiesCommand;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Queries.Properties;

namespace RealEstate.Properties.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] string name,
            [FromQuery] string address,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var query = new GetPropertiesQuery(name, address, minPrice, maxPrice);
            var result = await _mediator.Send(query);

            if (result == null || !result.Any())
                return Ok(new { message = "No hay propiedades registradas con esos criterios.", data = new List<PropertyDto>() });

            return Ok(new { message = "Propiedades obtenidas correctamente.", data = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var property = await _mediator.Send(new GetPropertyByIdQuery(id));
            if (property == null)
                return NotFound(new { message = "Propiedad no encontrada.", data = (PropertyDto?)null });
            return Ok(new { message = "Propiedad obtenida correctamente.", data = property });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PropertyDto dto)
        {
            var result = await _mediator.Send(new CreatePropertyCommand(dto));
            if (result == null)
                return BadRequest(new { message = "No se pudo crear la propiedad.", data = (PropertyDto?)null });
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { message = "Propiedad creada correctamente.", data = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] PropertyDto dto)
        {
            dto.Id = id;
            var result = await _mediator.Send(new UpdatePropertyCommand(dto));
            if (result == null)
                return NotFound(new { message = "Propiedad no encontrada.", data = (PropertyDto?)null });
            return Ok(new { message = "Propiedad actualizada correctamente.", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _mediator.Send(new DeletePropertyCommand(id));
            if (!success)
                return NotFound(new { message = "Propiedad no encontrada o no se pudo eliminar." });
            return Ok(new { message = "Propiedad eliminada correctamente." });
        }
    }
}
