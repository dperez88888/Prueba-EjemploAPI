using Microsoft.EntityFrameworkCore;
using PruebaExperticket_Backend.Mapper;
using PruebaExperticket_Backend.Model;
using PruebaExperticket_Backend.Repository;

namespace PruebaExperticket_Backend.Services
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
            return _clienteRepository.GetCliente(clienteId);
        }

        public async Task UpdateCliente(Cliente cliente)
        {            
            _clienteRepository.UpdateCliente(ClienteEntityMapper.Map(cliente));
        }
    }
}
