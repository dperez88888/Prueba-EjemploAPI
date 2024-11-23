using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Application.Interface.Persistence
{
    public interface IClienteRepository
    {
        //Métodos síncronos
        bool AddCliente(Cliente cliente);

        bool DeleteCliente(int clienteId);

        List<Cliente> GetClientes();

        Cliente? GetCliente(int clienteId);

        bool UpdateCliente(Cliente cliente);

        // Métodos Asíncronos

        Task<bool> AddClienteAsync(Cliente cliente);

        Task<bool> DeleteClienteAsync(int clienteId);

        Task<List<Cliente>> GetClientesAsync();

        Task<Cliente?> GetClienteAsync(int clienteId);

        Task<bool> UpdateClienteAsync(Cliente cliente);

    }
}
