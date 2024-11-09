using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Dominio.DTO;
using PruebaEjemploAPI_Backend.Transversal.Common;
using PruebaEjemploAPI_Backend.Transversal.Exceptions;
using PruebaEjemploAPI_Backend.Aplicacion.Validators;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {

        private readonly IUsuarioDomService _usuarioDomService;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _usuarioValidator;
        private readonly IAppLogger<IUsuarioAppService> _logger;

        public UsuarioAppService(IUsuarioDomService usuarioDomService, IMapper mapper, UsuarioValidator usuarioValidator, IAppLogger<IUsuarioAppService> logger)
        {
            _usuarioDomService = usuarioDomService;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
            _logger = logger;
        }

        public Response<bool> AddUsuario(UsuarioDTO usuario)
        {
            var res = new Response<bool>();

            try
            {                
                var usr = _mapper.Map<UsuarioDTO, UsuarioDomDTO>(usuario);

                res.Data = _usuarioDomService.AddUsuario(usr);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + usr.Nombre + " " + usr.Apellidos);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuario.Nombre + " " + usuario.Apellidos);
            }

            return res; 
        }

        public async Task<Response<bool>> AddUsuarioAsync(UsuarioDTO usuario)
        {
            var res = new Response<bool>();

            try
            {
                var usr = _mapper.Map<UsuarioDTO, UsuarioDomDTO>(usuario);

                res.Data = await _usuarioDomService.AddUsuarioAsync(usr);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + usr.Nombre + " " + usr.Apellidos);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuario.Nombre + " " + usuario.Apellidos);
            }

            return res;
        }

        public Response<bool> DeleteUsuario(int usuarioId)
        {
            var res = new Response<bool>();

            try
            {                
                res.Data = _usuarioDomService.DeleteUsuario(usuarioId);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Borrado con éxito";
                    _logger.LogInfo(res.Message + " " + usuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuarioId);
            }

            return res;
        }

        public async Task<Response<bool>> DeleteUsuarioAsync(int usuarioId)
        {
            var res = new Response<bool>();

            try
            {
                res.Data = await _usuarioDomService.DeleteUsuarioAsync(usuarioId);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Borrado con éxito";
                    _logger.LogInfo(res.Message + " " + usuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuarioId);
            }

            return res;
        }

        public Response<UsuarioDTO> GetUsuario(int usuarioId)
        {
            var res = new Response<UsuarioDTO>();

            try
            {                
                res.Data = _mapper.Map<UsuarioDomDTO, UsuarioDTO> (_usuarioDomService.GetUsuario(usuarioId));
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Obtenido con éxito";
                    _logger.LogInfo(res.Message + " " + usuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuarioId);
            }

            return res;
        }

        public async Task<Response<UsuarioDTO>> GetUsuarioAsync(int usuarioId)
        {
            var res = new Response<UsuarioDTO>();

            try
            {
                res.Data = _mapper.Map<UsuarioDomDTO, UsuarioDTO>(await _usuarioDomService.GetUsuarioAsync(usuarioId));
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Obtenido con éxito";
                    _logger.LogInfo(res.Message + " " + usuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuarioId);
            }

            return res;
        }

        public Response<List<UsuarioDTO>> GetUsuarios()
        {
            var res = new Response<List<UsuarioDTO>>();

            try
            {                
                res.Data = _mapper.Map<List<UsuarioDomDTO>, List<UsuarioDTO>>(_usuarioDomService.GetUsuarios());
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuarios Obtenidos con éxito";
                    _logger.LogInfo(res.Message);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message);
            }

            return res;
        }

        public async Task<Response<List<UsuarioDTO>>> GetUsuariosAsync()
        {
            var res = new Response<List<UsuarioDTO>>();

            try
            {
                res.Data = _mapper.Map<List<UsuarioDomDTO>, List<UsuarioDTO>>(await _usuarioDomService.GetUsuariosAsync());
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuarios Obtenidos con éxito";
                    _logger.LogInfo(res.Message);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message);
            }

            return res;
        }

        public Response<bool> UpdateUsuario(UsuarioDTO usuario)
        {
            var res = new Response<bool>();

            try
            {
                var usr = _mapper.Map<UsuarioDTO, UsuarioDomDTO>(usuario);

                res.Data = _usuarioDomService.UpdateUsuario(usr);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + usr.UsuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuario.UsuarioId);
            }

            return res;
        }

        public async Task<Response<bool>> UpdateUsuarioAsync(UsuarioDTO usuario)
        {
            var res = new Response<bool>();

            try
            {
                var usr = _mapper.Map<UsuarioDTO, UsuarioDomDTO>(usuario);

                res.Data = await _usuarioDomService.UpdateUsuarioAsync(usr);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + usr.UsuarioId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + usuario.UsuarioId);
            }

            return res;
        }

        public Response<UsuarioDTO> Authenticate(string nombre, string password)
        {
            var response = new Response<UsuarioDTO>();

            var validation = _usuarioValidator.Validate(new UsuarioDTO() { Nombre = nombre, Password = password, Apellidos = "x"});

            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.IsSuccess = false;
                response.Errors = validation.Errors;
                _logger.LogError(response.Message + " " + response.Errors + " " + nombre + " " + password);

            }
            else
            {
                try
                {
                    var user = _usuarioDomService.Authenticate(nombre, password);
                    response.Data = _mapper.Map<UsuarioDomDTO, UsuarioDTO>(user);
                    response.IsSuccess = true;
                    response.Message = "Autenticación válida";
                    _logger.LogInfo(response.Message + " " + nombre + " " + password);
                }
                catch(UsuarioNotFoundException  ex)
                {
                    response.IsSuccess = true;
                    response.Message = ex.Message;
                    _logger.LogError(response.Message + " " + nombre + " " + password);
                }
                catch(Exception ex)
                {
                    response.Message = ex.Message;
                    _logger.LogError(response.Message + " " + nombre + " " + password);
                    throw;
                }
            }

            return response;
        }                
    }
}
