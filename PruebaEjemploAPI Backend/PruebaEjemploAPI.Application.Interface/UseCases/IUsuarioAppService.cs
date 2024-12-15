using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.Interface.UseCases
{
    public interface IUsuarioAppService
    {
        //Métodos síncronos
        Response<bool> AddUsuario(UsuarioDTO usuario);

        Response<bool> DeleteUsuario(int usuarioId);

        Response<List<UsuarioDTO>> GetUsuarios();

        Response<UsuarioDTO> GetUsuario(int usuarioId);

        Response<bool> UpdateUsuario(UsuarioDTO usuario);

        Response<UsuarioDTO> Authenticate(string nombre, string password);

        // Métodos Asíncronos

        Task<Response<bool>> AddUsuarioAsync(UsuarioDTO usuario);

        Task<Response<bool>> DeleteUsuarioAsync(int usuarioId);

        Task<Response<List<UsuarioDTO>>> GetUsuariosAsync();

        Task<Response<UsuarioDTO>> GetUsuarioAsync(int usuarioId);

        Task<Response<bool>> UpdateUsuarioAsync(UsuarioDTO usuario);

        Task<Response<UsuarioDTO>> AuthenticateAsync(string nombre, string password);

    }
}
