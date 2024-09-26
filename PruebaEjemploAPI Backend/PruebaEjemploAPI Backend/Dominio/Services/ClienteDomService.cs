using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Dominio.DTO;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using AutoMapper.Internal;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public class ClienteDomService : IClienteDomService
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteDomService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
               
        
        public bool AddCliente(ClienteDomDTO cliente)
        {
            var cli = _mapper.Map<ClienteDomDTO, Cliente>(cliente);

            return _clienteRepository.AddCliente(cli);

        }

        public bool DeleteCliente(int clienteId)
        {
            return _clienteRepository.DeleteCliente(clienteId);
        }

        public List<ClienteDomDTO> GetClientes()
        {
            var clientes = _clienteRepository.GetClientes();
            
            return _mapper.Map<List<Cliente>, List<ClienteDomDTO>>(clientes);
            
        }

        public ClienteDomDTO GetCliente(int clienteId)
        {
            var cli = _clienteRepository.GetCliente(clienteId);

            return _mapper.Map<Cliente, ClienteDomDTO>(cli);            
        }

        public bool UpdateCliente(ClienteDomDTO cliente)
        {
            var cli = _mapper.Map<ClienteDomDTO, Cliente>(cliente);
            return _clienteRepository.UpdateCliente(cli);
        }

        public async Task<bool> AddClienteAsync(ClienteDomDTO cliente)
        {
            var cli = _mapper.Map<ClienteDomDTO, Cliente>(cliente);

            return await _clienteRepository.AddClienteAsync(cli);
        }

        public async Task<bool> DeleteClienteAsync(int clienteId)
        {
            return await _clienteRepository.DeleteClienteAsync(clienteId);
        }

        public async Task<List<ClienteDomDTO>> GetClientesAsync()
        {
            var clientes = await _clienteRepository.GetClientesAsync();

            return _mapper.Map<List<Cliente>, List<ClienteDomDTO>>(clientes);
        }

        public async Task<ClienteDomDTO> GetClienteAsync(int clienteId)
        {
            var cli = await _clienteRepository.GetClienteAsync(clienteId);

            return _mapper.Map<Cliente, ClienteDomDTO>(cli);
        }

        public async Task<bool> UpdateClienteAsync(ClienteDomDTO cliente)
        {
            var cli = _mapper.Map<ClienteDomDTO, Cliente>(cliente);
            return await _clienteRepository.UpdateClienteAsync(cli);
        }
    }
}
