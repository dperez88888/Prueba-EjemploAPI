using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.UseCases;

namespace PruebaEjemploAPI.Service.Controllers.v1
{
    [Authorize]
    [Route("PruebaEjemploAPI/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ClientesController : ControllerBase
    {

        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteAppService _clienteService;

        public ClientesController(ILogger<ClientesController> logger, IClienteAppService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        // Métodos Síncronos

        // GET PruebaEjemploAPI/clientes/get/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = _clienteService.GetCliente(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/clientes/get
        [HttpGet]
        public IActionResult Get()
        {
            var res = _clienteService.GetClientes();

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/clientes/Post
        [HttpPost]
        public IActionResult Post([FromBody] ClienteDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = _clienteService.AddCliente(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/clientes/5
        [HttpPut]
        public IActionResult Put([FromBody] ClienteDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = _clienteService.UpdateCliente(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/clientes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = _clienteService.DeleteCliente(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // Métodos asíncronos

        // GET PruebaEjemploAPI/clientes/5
        [HttpGet("async/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _clienteService.GetClienteAsync(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/clientes
        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _clienteService.GetClientesAsync();

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/clientes
        [HttpPost("async")]
        public async Task<IActionResult> PostAsync([FromBody] ClienteDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = await _clienteService.AddClienteAsync(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/clientes/5
        [HttpPut("async")]
        public async Task<IActionResult> PutAsync([FromBody] ClienteDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = await _clienteService.UpdateClienteAsync(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/clientes/5
        [HttpDelete("async/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _clienteService.DeleteClienteAsync(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


    }
}
