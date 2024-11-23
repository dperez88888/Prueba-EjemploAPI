

using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Application.Interface.Persistence
{
    public interface IUsuarioRepository
    {
        //Métodos síncronos
        bool AddUsuario(Usuario usuario);

        bool DeleteUsuario(int usuarioId);

        List<Usuario> GetUsuarios();

        Usuario? GetUsuario(int usuarioId);

        bool UpdateUsuario(Usuario usuario);

        Usuario Authenticate(string nombre, string password);

        // Métodos Asíncronos

        Task<bool> AddUsuarioAsync(Usuario usuario);

        Task<bool> DeleteUsuarioAsync(int usuarioId);

        Task<List<Usuario>> GetUsuariosAsync();

        Task<Usuario?> GetUsuarioAsync(int usuarioId);

        Task<bool> UpdateUsuarioAsync(Usuario usuario);



    }
}
