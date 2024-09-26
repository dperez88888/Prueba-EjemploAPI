using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public interface IClienteAppService
    {
        //Métodos síncronos
        Response<bool> AddCliente(ClienteDTO cliente);

        Response<bool> DeleteCliente(int clienteId);

        Response<List<ClienteDTO>> GetClientes();

        Response<ClienteDTO> GetCliente(int clienteId);

        Response<bool>UpdateCliente(ClienteDTO cliente);

        // Métodos Asíncronos

        Task<Response<bool>> AddClienteAsync(ClienteDTO cliente);

        Task<Response<bool>> DeleteClienteAsync(int clienteId);

        Task<Response<List<ClienteDTO>>> GetClientesAsync();

        Task<Response<ClienteDTO>> GetClienteAsync(int clienteId);

        Task<Response<bool>>UpdateClienteAsync(ClienteDTO cliente);

    }
}
