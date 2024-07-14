using Microsoft.AspNetCore.Mvc;
using PruebaEjemploAPI_Backend.Model;
using PruebaEjemploAPI_Backend.Requests;
using PruebaEjemploAPI_Backend.Services;

namespace PruebaEjemploAPI_Backend.Controllers
{
    [Route("PruebaEjemploAPI/[controller]")]
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

        // GET PruebaEjemploAPI/clientes/5
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

        // GET PruebaEjemploAPI/clientes
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


        // POST PruebaEjemploAPI/clientes
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

        // PUT PruebaEjemploAPI/clientes/5
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
                
        // DELETE PruebaEjemploAPI/clientes/5
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
