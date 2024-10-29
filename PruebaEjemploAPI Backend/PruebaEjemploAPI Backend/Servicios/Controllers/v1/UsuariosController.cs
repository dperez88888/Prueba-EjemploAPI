using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Dominio.Services;
using PruebaEjemploAPI_Backend.Transversal.Settings;

namespace PruebaEjemploAPI_Backend.Servicios.Controllers.v1
{
    [Authorize]
    [Route("PruebaEjemploAPI/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsuariosController : ControllerBase
    {

        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioAppService _usuarioService;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioAppService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        // Métodos Síncronos
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = _usuarioService.Authenticate(usuarioDTO.Nombre, usuarioDTO.Password);

            if (response.IsSuccess)
            {
                return response.Data != null ? Ok(response) : NotFound(response.Message);
            }

            return BadRequest(response);
        }

        // GET PruebaEjemploAPI/usuarios/get/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = _usuarioService.GetUsuario(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/usuarios/get
        [HttpGet]
        public IActionResult Get()
        {
            var res = _usuarioService.GetUsuarios();

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/usuarios/Post
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = _usuarioService.AddUsuario(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/usuarios/5
        [HttpPut]
        public IActionResult Put([FromBody] UsuarioDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = _usuarioService.UpdateUsuario(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/usuarios/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = _usuarioService.DeleteUsuario(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // Métodos asíncronos

        // GET PruebaEjemploAPI/usuarios/5
        [HttpGet("async/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _usuarioService.GetUsuarioAsync(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // GET PruebaEjemploAPI/usuarios
        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _usuarioService.GetUsuariosAsync();

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


        // POST PruebaEjemploAPI/usuarios
        [HttpPost("async")]
        public async Task<IActionResult> PostAsync([FromBody] UsuarioDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = await _usuarioService.AddUsuarioAsync(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);

        }

        // PUT PruebaEjemploAPI/usuarios/5
        [HttpPut("async")]
        public async Task<IActionResult> PutAsync([FromBody] UsuarioDTO request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var res = await _usuarioService.UpdateUsuarioAsync(request);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        // DELETE PruebaEjemploAPI/usuarios/5
        [HttpDelete("async/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var res = await _usuarioService.DeleteUsuarioAsync(id);

            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }


    }

}
