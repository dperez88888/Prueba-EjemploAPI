using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Context;
using PruebaEjemploAPI_Backend.Infraestructura.Model;

namespace PruebaEjemploAPI_Backend.Infraestructura.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IContextDB _contextDB;

        public ClienteRepository(IContextDB contextDB)
        {
            _contextDB = contextDB;
        }
         
        bool IClienteRepository.AddCliente(Cliente cliente)
        {
            _contextDB.Clientes.Add(cliente);

            var result = _contextDB.SaveChangesAsync()?.Result;

            return result > 0;
        }

        bool IClienteRepository.DeleteCliente(int clienteId)
        {
            var entity = GetCliente(clienteId);
            if (entity != null)
            {
                _contextDB.Clientes.Remove(entity);
                var result = _contextDB.SaveChangesAsync()?.Result;
                return result > 0;
            }

            return true;
        }

        public Cliente? GetCliente(int id)
        {
            return _contextDB.Clientes.Find(id);
        }

        List<Cliente> IClienteRepository.GetClientes()
        {
            return _contextDB.Clientes.ToList();
        }

        bool IClienteRepository.UpdateCliente(Cliente cliente)
        {
            var cli = _contextDB.Clientes.Update(cliente);
            if (cli != null)
            {
                var result = _contextDB.SaveChangesAsync()?.Result;
                return result > 0;
            }

            return false;
        }

        public async Task<bool> AddClienteAsync(Cliente cliente)
        {
            await _contextDB.Clientes.AddAsync(cliente);
            
            var result = await _contextDB.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Cliente?> GetClienteAsync(int id)
        {
            return await _contextDB.Clientes.FindAsync(id);
        }

        public async Task<bool> DeleteClienteAsync(int clienteId)
        {
            var entity = GetCliente(clienteId);
            if (entity != null)
            {
                _contextDB.Clientes.Remove(entity);
                var result = await _contextDB.SaveChangesAsync();
                return result > 0;
            }

            return true;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _contextDB.Clientes.ToListAsync();
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente)
        {
            var cli = _contextDB.Clientes.Update(cliente);
            if (cli != null)
            {
               var result = await _contextDB.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }
}
