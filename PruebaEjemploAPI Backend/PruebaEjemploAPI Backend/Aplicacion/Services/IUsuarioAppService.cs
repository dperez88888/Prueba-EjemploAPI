using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public interface IUsuarioAppService
    {
        //Métodos síncronos
        Response<bool> AddUsuario(UsuarioDTO usuario);

        Response<bool> DeleteUsuario(int usuarioId);

        Response<List<UsuarioDTO>> GetUsuarios();

        Response<UsuarioDTO> GetUsuario(int usuarioId);

        Response<bool>UpdateUsuario(UsuarioDTO usuario);

        Response<UsuarioDTO> Authenticate(string nombre, string password);

        // Métodos Asíncronos

        Task<Response<bool>> AddUsuarioAsync(UsuarioDTO usuario);

        Task<Response<bool>> DeleteUsuarioAsync(int usuarioId);

        Task<Response<List<UsuarioDTO>>> GetUsuariosAsync();

        Task<Response<UsuarioDTO>> GetUsuarioAsync(int usuarioId);

        Task<Response<bool>>UpdateUsuarioAsync(UsuarioDTO usuario);

    }
}
