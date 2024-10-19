using PruebaEjemploAPI_Backend.Dominio.DTO;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public interface IUsuarioDomService
    {        
        //Métodos síncronos
        bool AddUsuario(UsuarioDomDTO usuario);

        bool DeleteUsuario(int usuarioId);

        List<UsuarioDomDTO> GetUsuarios();

        UsuarioDomDTO? GetUsuario(int usuarioId);

        bool UpdateUsuario(UsuarioDomDTO usuario);

        UsuarioDomDTO Authenticate(string nombre, string password);

        // Métodos Asíncronos

        Task<bool> AddUsuarioAsync(UsuarioDomDTO usuario);

        Task<bool> DeleteUsuarioAsync(int usuarioId);

        Task<List<UsuarioDomDTO>> GetUsuariosAsync();

        Task<UsuarioDomDTO?> GetUsuarioAsync(int usuarioId);

        Task<bool> UpdateUsuarioAsync(UsuarioDomDTO usuario);



    }
}
