using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Dominio.DTO;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using AutoMapper.Internal;
using Microsoft.Identity.Client;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public class UsuarioDomService : IUsuarioDomService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioDomService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
               
        
        public bool AddUsuario(UsuarioDomDTO usuario)
        {
            var usr = _mapper.Map<UsuarioDomDTO, Usuario>(usuario);

            return _usuarioRepository.AddUsuario(usr);

        }

        public bool DeleteUsuario(int usuarioId)
        {
            return _usuarioRepository.DeleteUsuario(usuarioId);
        }

        public List<UsuarioDomDTO> GetUsuarios()
        {
            var usuarios = _usuarioRepository.GetUsuarios();
            
            return _mapper.Map<List<Usuario>, List<UsuarioDomDTO>>(usuarios);
            
        }

        public UsuarioDomDTO GetUsuario(int usuarioId)
        {
            var usr = _usuarioRepository.GetUsuario(usuarioId);

            return _mapper.Map<Usuario, UsuarioDomDTO>(usr);            
        }

        public bool UpdateUsuario(UsuarioDomDTO usuario)
        {
            var usr = _mapper.Map<UsuarioDomDTO, Usuario>(usuario);
            return _usuarioRepository.UpdateUsuario(usr);
        }

        public UsuarioDomDTO Authenticate(string nombre, string password)
        {
            var usr = _usuarioRepository.Authenticate(nombre, password);
            return _mapper.Map<Usuario, UsuarioDomDTO>(usr);
        }

        public async Task<bool> AddUsuarioAsync(UsuarioDomDTO usuario)
        {
            var usr = _mapper.Map<UsuarioDomDTO, Usuario>(usuario);

            return await _usuarioRepository.AddUsuarioAsync(usr);
        }

        public async Task<bool> DeleteUsuarioAsync(int usuarioId)
        {
            return await _usuarioRepository.DeleteUsuarioAsync(usuarioId);
        }

        public async Task<List<UsuarioDomDTO>> GetUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();

            return _mapper.Map<List<Usuario>, List<UsuarioDomDTO>>(usuarios);
        }

        public async Task<UsuarioDomDTO> GetUsuarioAsync(int usuarioId)
        {
            var usr = await _usuarioRepository.GetUsuarioAsync(usuarioId);

            return _mapper.Map<Usuario, UsuarioDomDTO>(usr);
        }

        public async Task<bool> UpdateUsuarioAsync(UsuarioDomDTO usuario)
        {
            var usr = _mapper.Map<UsuarioDomDTO, Usuario>(usuario);
            return await _usuarioRepository.UpdateUsuarioAsync(usr);
        }
    }
}
