using PruebaEjemploAPI_Backend.Dominio.DTO;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public interface IClienteDomService
    {
        //Métodos síncronos
        bool AddCliente(ClienteDomDTO cliente);

        bool DeleteCliente(int clienteId);

        List<ClienteDomDTO> GetClientes();

        ClienteDomDTO GetCliente(int clienteId);

        bool UpdateCliente(ClienteDomDTO cliente);

        // Métodos Asíncronos

        Task<bool> AddClienteAsync(ClienteDomDTO cliente);

        Task<bool> DeleteClienteAsync(int clienteId);

        Task<List<ClienteDomDTO>> GetClientesAsync();

        Task<ClienteDomDTO> GetClienteAsync(int clienteId);

        Task<bool> UpdateClienteAsync(ClienteDomDTO cliente);

    }
}
