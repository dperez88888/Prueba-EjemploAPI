using AutoMapper;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Transversal.Common;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Exceptions;

namespace PruebaEjemploAPI.Application.UseCases
{
    public class UsuarioAppService : IUsuarioAppService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _usuarioValidator;
        private readonly IAppLogger<IUsuarioAppService> _logger;

        public UsuarioAppService(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator usuarioValidator, IAppLogger<IUsuarioAppService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
            _logger = logger;
        }

        public Response<bool> AddUsuario(UsuarioDTO usuario)
        {
            var res = new Response<bool>();

            try
            {                
                var usr = _mapper.Map<UsuarioDTO, Usuario>(usuario);

                res.Data = _unitOfWork.UsuarioRepository.AddUsuario(usr);
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
                var usr = _mapper.Map<UsuarioDTO, Usuario>(usuario);

                res.Data = await _unitOfWork.UsuarioRepository.AddUsuarioAsync(usr);
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
                res.Data = _unitOfWork.UsuarioRepository.DeleteUsuario(usuarioId);
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
                res.Data = await _unitOfWork.UsuarioRepository.DeleteUsuarioAsync(usuarioId);
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
                res.Data = _mapper.Map<Usuario, UsuarioDTO> (_unitOfWork.UsuarioRepository.GetUsuario(usuarioId));
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
                res.Data = _mapper.Map<Usuario, UsuarioDTO>(await _unitOfWork.UsuarioRepository.GetUsuarioAsync(usuarioId));
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
                res.Data = _mapper.Map<List<Usuario>, List<UsuarioDTO>>(_unitOfWork.UsuarioRepository.GetUsuarios());
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
                res.Data = _mapper.Map<List<Usuario>, List<UsuarioDTO>>(await _unitOfWork.UsuarioRepository.GetUsuariosAsync());
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
                var usr = _mapper.Map<UsuarioDTO, Usuario>(usuario);

                res.Data = _unitOfWork.UsuarioRepository.UpdateUsuario(usr);
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
                var usr = _mapper.Map<UsuarioDTO, Usuario>(usuario);

                res.Data = await _unitOfWork.UsuarioRepository.UpdateUsuarioAsync(usr);
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
                    var user = _unitOfWork.UsuarioRepository.Authenticate(nombre, password);
                    response.Data = _mapper.Map<Usuario, UsuarioDTO>(user);
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
