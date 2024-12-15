using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using MediatR;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuarioQuery;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuariosQuery;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.AddUsuarioCommand;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.DeleteUsuarioCommand;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.AuthenticateQuery;

namespace PruebaEjemploAPI.Service.Controllers.v3
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("PruebaEjemploAPI/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class UsuariosController : ControllerBase
    {

        private readonly ILogger<UsuariosController> _logger;
        private readonly IMediator _mediator;

        public UsuariosController(ILogger<UsuariosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
                
        // Sólo Métodos asíncronos para la v3

        // GET PruebaEjemploAPI/usuarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(new GetUsuarioQuery() { UsuarioId = id });

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/usuarios
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _mediator.Send(new GetUsuariosQuery());

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/usuarios
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddUsuarioCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(command);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/usuarios/5
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateUsuarioCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(command);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _mediator.Send(new DeleteUsuarioCommand() { UsuarioId = id });

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateQuery query)
        {
            if ( query is null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(query);

            if (response.IsSuccess)
            {
                return response.Data != null ? Ok(response) : NotFound(response.Message);
            }

            return BadRequest(response);
        }


    }

}
