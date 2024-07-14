using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Mapper;
using PruebaEjemploAPI_Backend.Model;
using PruebaEjemploAPI_Backend.Repository;

namespace PruebaEjemploAPI_Backend.Services
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task AddCliente(Cliente cliente)
        {
            await _clienteRepository.AddCliente(ClienteEntityMapper.Map(cliente));
        }

        public async Task DeleteCliente(int clienteId)
        {
            await _clienteRepository.DeleteCliente(clienteId);
        }
        

        public async Task<List<Cliente>> GetClientes()
        {
            return await _clienteRepository.GetClientes();
        }

        public Task<Cliente> GetCliente(int clienteId)
        {
            return _clienteRepository.GetClienteAsync(clienteId);
        }

        public async Task UpdateCliente(Cliente cliente)
        {            
            _clienteRepository.UpdateCliente(ClienteEntityMapper.Map(cliente));
        }
    }
}
