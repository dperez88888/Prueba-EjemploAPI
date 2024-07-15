using PruebaEjemploAPI_Backend.Model;

namespace PruebaEjemploAPI_Backend.Repository
{
    public interface IClienteRepository
    {
        Task AddCliente(Cliente cliente);

        Task<Cliente> GetClienteAsync(int id);

        Cliente GetCliente(int id);

        Task<List<Cliente>> GetClientes();

        Task DeleteCliente(int id);
        
        void UpdateCliente(Cliente cliente);    

    }
}
