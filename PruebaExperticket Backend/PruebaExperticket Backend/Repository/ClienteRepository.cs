using Microsoft.EntityFrameworkCore;
using PruebaExperticket_Backend.Context;
using PruebaExperticket_Backend.Model;

namespace PruebaExperticket_Backend.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IContextDB _contextDB;

        public ClienteRepository(IContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task AddCliente(Cliente cliente)
        {
            await _contextDB.Clientes.AddAsync(cliente);
            await _contextDB.SaveChangesAsync();
        }

        public async Task DeleteCliente(int id)
        {
            var entity = GetCliente(id);

            _contextDB.Clientes.Remove(entity);
            _contextDB.SaveChangesAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _contextDB.Clientes.FindAsync(id);
        }

        public Cliente GetCliente(int id)
        {
            return _contextDB.Clientes.Find(id);
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _contextDB.Clientes.ToListAsync();
        }

        public void UpdateCliente(Cliente cliente)
        {                            
            _contextDB.Clientes.Update(cliente);
            _contextDB.SaveChangesAsync(); 
        }
    }
}
