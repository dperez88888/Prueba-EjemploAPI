using Microsoft.AspNetCore.Mvc;
using PruebaExperticket_Backend.Model;
using PruebaExperticket_Backend.Requests;
using PruebaExperticket_Backend.Services;

namespace PruebaExperticket_Backend.Controllers
{
    [Route("PruebaExperticket/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {              

        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteService _clienteService;

        public ClientesController(ILogger<ClientesController> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        // GET PruebaExperticket/clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var entity = await _clienteService.GetCliente(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode (500, ex.Message);
            }            
        }

        // GET PruebaExperticket/clientes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entity = await _clienteService.GetClientes();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // POST PruebaExperticket/clientes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteRequest request)
        {
            try {
                var model = new Cliente
                {
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    CodigoPostal = request.CodigoPostal,
                    Sexo = request.Sexo,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion,
                    Pais = request.Pais,
                    Email = request.Email
                };

                await _clienteService.AddCliente(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        // PUT PruebaExperticket/clientes/5
        [HttpPut]
        public IActionResult Put([FromBody] ClienteRequest request)
        {
            try
            {
                var model = new Cliente
                {
                    ClienteId = request.ClienteId,
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    CodigoPostal = request.CodigoPostal,
                    Sexo = request.Sexo,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion,
                    Pais = request.Pais,
                    Email = request.Email
                };

                _clienteService.UpdateCliente(model);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
                
        // DELETE PruebaExperticket/clientes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _clienteService.DeleteCliente(id);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
