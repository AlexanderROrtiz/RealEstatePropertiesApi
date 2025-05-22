using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Properties.Application.Commands.OwnersCommand;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Queries.Owners;

namespace RealEstate.Properties.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new GetOwnersQuery());
            if (result == null || !result.Any())
                return Ok(new { message = "No hay propietarios registrados.", data = new List<OwnerDto>() });

            return Ok(new { message = "Propietarios obtenidos correctamente.", data = result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var owner = await _mediator.Send(new GetOwnerByIdQuery(id));
            if (owner == null)
                return NotFound(new { message = "Propietario no encontrado.", data = (OwnerDto?)null });
            return Ok(new { message = "Propietario obtenido correctamente.", data = owner });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OwnerDto dto)
        {
            var result = await _mediator.Send(new CreateOwnerCommand(dto));
            if (result == null)
                return BadRequest(new { message = "No se pudo crear el propietario.", data = (OwnerDto?)null });
            return CreatedAtAction(nameof(GetById), new { id = result.IdOwner }, new { message = "Propietario creado correctamente.", data = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] OwnerDto dto)
        {
            dto.IdOwner = id;
            var result = await _mediator.Send(new UpdateOwnerCommand(dto));
            if (result == null)
                return NotFound(new { message = "Propietario no encontrado.", data = (OwnerDto?)null });
            return Ok(new { message = "Propietario actualizado correctamente.", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _mediator.Send(new DeleteOwnerCommand(id));
            if (!success)
                return NotFound(new { message = "Propietario no encontrado o no se pudo eliminar." });
            return Ok(new { message = "Propietario eliminado correctamente." });
        }
    }
}