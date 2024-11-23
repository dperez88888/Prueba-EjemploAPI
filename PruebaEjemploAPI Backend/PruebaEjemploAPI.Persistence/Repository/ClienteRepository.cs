using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI.Persistence.Context;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Persistence.Repository
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
            if (cliente != null)
            {
                _contextDB.Clientes.Add(cliente);

                var result = _contextDB.SaveChangesAsync()?.Result;

                return result > 0;
            }

            return false;
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

            return false;
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
            if (cliente != null)
            {
                var cli = _contextDB.Clientes.Update(cliente);
                if (cli != null)
                {
                    var result = _contextDB.SaveChangesAsync()?.Result;
                    return result > 0;
                }
            }

            return false;

        }

        public async Task<bool> AddClienteAsync(Cliente cliente)
        {
            if (cliente != null)
            {
                await _contextDB.Clientes.AddAsync(cliente);

                var result = await _contextDB.SaveChangesAsync();

                return result > 0;
            }

            return false;
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
            if (cliente != null)
            {
                var cli = _contextDB.Clientes.Update(cliente);
                if (cli != null)
                {
                    var result = await _contextDB.SaveChangesAsync();
                    return result > 0;
                }

            }

            return false;
        }
    }
}
