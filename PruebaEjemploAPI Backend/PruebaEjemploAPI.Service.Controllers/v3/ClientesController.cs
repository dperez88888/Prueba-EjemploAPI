using Asp.Versioning;
using LiteDB;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using PruebaEjemploAPI.Application.UseCases.Clientes.Commands.AddClienteCommand;
using PruebaEjemploAPI.Application.UseCases.Clientes.Commands.DeleteClienteCommand;
using PruebaEjemploAPI.Application.UseCases.Clientes.Commands.UpdateClienteCommand;
using PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClienteQuery;
using PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClientesQuery;

namespace PruebaEjemploAPI.Service.Controllers.v3
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("PruebaEjemploAPI/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class ClientesController : ControllerBase
    {

        private readonly ILogger<ClientesController> _logger;
        private readonly IMediator _mediator;

        public ClientesController(ILogger<ClientesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // En el v3 sólo dejamos métodos asíncronos para el uso de los patrones CQRS y Mediator

        // GET PruebaEjemploAPI/clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int clienteId)
        {
            if (clienteId < 0)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(new GetClienteQuery() { ClienteId = clienteId});

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/clientes
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            
            var res = await _mediator.Send(new GetClientesQuery());

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/clientes
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddClienteCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(command);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/clientes/5
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateClienteCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(command);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int clienteId)
        {
            if (clienteId < 0)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(new DeleteClienteCommand() { ClienteId = clienteId});

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


    }
}
