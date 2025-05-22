using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Properties.Application.Commands.PropertyTraceCommand;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Queries.PropertyTrace;

namespace RealEstate.Properties.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTracesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyTracesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult> GetByProperty(string propertyId)
        {
            var query = new GetPropertyTracesByPropertyIdQuery(propertyId);
            var result = await _mediator.Send(query);

            if (result == null || !result.Any())
                return Ok(new { message = "No hay trazas para esta propiedad.", data = new List<PropertyTraceDto>() });

            return Ok(new { message = "Trazas de propiedad obtenidas correctamente.", data = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            // Implementa el query si se necesita
            return NotFound(new { message = "Traza no encontrada.", data = (PropertyTraceDto?)null });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PropertyTraceDto dto)
        {
            var result = await _mediator.Send(new CreatePropertyTraceCommand(dto));
            if (result == null)
                return BadRequest(new { message = "No se pudo crear la traza.", data = (PropertyTraceDto?)null });
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { message = "Traza creada correctamente.", data = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] PropertyTraceDto dto)
        {
            dto.Id = id;
            var result = await _mediator.Send(new UpdatePropertyTraceCommand(dto));
            if (result == null)
                return NotFound(new { message = "Traza no encontrada.", data = (PropertyTraceDto?)null });
            return Ok(new { message = "Traza actualizada correctamente.", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _mediator.Send(new DeletePropertyTraceCommand(id));
            if (!success)
                return NotFound(new { message = "Traza no encontrada o no se pudo eliminar." });
            return Ok(new { message = "Traza eliminada correctamente." });
        }
    }
}