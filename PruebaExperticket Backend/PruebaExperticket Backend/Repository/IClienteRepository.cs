using PruebaExperticket_Backend.Model;

namespace PruebaExperticket_Backend.Repository
{
    public interface IClienteRepository
    {
        Task AddCliente(Cliente cliente);

        Task<Cliente> GetCliente(int id);

        Task<List<Cliente>> GetClientes();

        Task DeleteCliente(int id);
        
        void UpdateCliente(Cliente cliente);    

    }
}
