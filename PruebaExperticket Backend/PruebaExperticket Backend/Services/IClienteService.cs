using PruebaEjemploAPI_Backend.Model;

namespace PruebaEjemploAPI_Backend.Services
{
    public interface IClienteService
    {
        Task AddCliente(Cliente cliente);

        Task DeleteCliente(int clienteId);

        Task<List<Cliente>> GetClientes();

        Task<Cliente> GetCliente(int clienteId);

        Task UpdateCliente(Cliente cancion);

    }
}
