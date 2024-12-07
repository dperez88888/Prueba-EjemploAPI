using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;
using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Application.UseCases
{
    public class ClienteAppService : IClienteAppService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClienteValidator _clienteValidator;
        private readonly IAppLogger<IClienteAppService> _logger;
        private readonly IDistributedCache _distributedCache;

        public ClienteAppService(IUnitOfWork unitOfWork, IMapper mapper, ClienteValidator clienteValidator, IAppLogger<IClienteAppService> logger, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clienteValidator = clienteValidator;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public Response<bool> AddCliente(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(cliente);

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;
                _logger.LogError(res.Message + " " + validation.Errors + " " + cliente.Nombre + " " + cliente.Apellidos);

            }
            else
            {
                var cli = _mapper.Map<ClienteDTO, Cliente>(cliente);

                res.Data = _unitOfWork.ClienteRepository.AddCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
                }
            }

            return res; 
        }

        public async Task<Response<bool>> AddClienteAsync(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(cliente);

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;
                _logger.LogError(res.Message + " " + validation.Errors + " " + cliente.Nombre + " " + cliente.Apellidos);
            }
            else
            {
                var cli = _mapper.Map<ClienteDTO, Cliente>(cliente);

                res.Data = await _unitOfWork.ClienteRepository.AddClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
                }
            }            

            return res;
        }

        public Response<bool> DeleteCliente(int clienteId)
        {
            var res = new Response<bool>();
                                      
            res.Data = _unitOfWork.ClienteRepository.DeleteCliente(clienteId);
            if (res.Data)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Borrado con éxito";
                _logger.LogInfo(res.Message + " " + clienteId);
            }

            return res;
        }

        public async Task<Response<bool>> DeleteClienteAsync(int clienteId)
        {
            var res = new Response<bool>();
            
            res.Data = await _unitOfWork.ClienteRepository.DeleteClienteAsync(clienteId);
            if (res.Data)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Borrado con éxito";
                _logger.LogInfo(res.Message + " " + clienteId);
            }

            return res;
        }

        public Response<ClienteDTO> GetCliente(int clienteId)
        {
            var res = new Response<ClienteDTO>();
                                      
            res.Data = _mapper.Map<Cliente, ClienteDTO> (_unitOfWork.ClienteRepository.GetCliente(clienteId));
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Obtenido con éxito";
                _logger.LogInfo(res.Message + " " + clienteId);
            }

            return res;
        }

        public async Task<Response<ClienteDTO>> GetClienteAsync(int clienteId)
        {
            var res = new Response<ClienteDTO>();
                        
            res.Data = _mapper.Map<Cliente, ClienteDTO>(await _unitOfWork.ClienteRepository.GetClienteAsync(clienteId));
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Obtenido con éxito";
                _logger.LogInfo(res.Message + " " + clienteId);
            }            

            return res;
        }

        public Response<List<ClienteDTO>> GetClientes()
        {
            var res = new Response<List<ClienteDTO>>();
            var cacheKey = "clientesList";
                           
            var redisClientes = _distributedCache.Get(cacheKey);
            if (redisClientes != null)
            {
                res.Data = JsonSerializer.Deserialize<List<ClienteDTO>>(redisClientes);
            }
            else
            {                    
                res.Data = _mapper.Map<List<Cliente>, List<ClienteDTO>>(_unitOfWork.ClienteRepository.GetClientes());
                if (res.Data != null)
                {
                    var serializedClientes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res.Data));
                    var opt = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                    _distributedCache.Set(cacheKey, serializedClientes, opt);
                }
            }
                
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Clientes Obtenidos con éxito";
                _logger.LogInfo(res.Message);
            }
            
            return res;
        }

        public async Task<Response<List<ClienteDTO>>> GetClientesAsync()
        {
            var res = new Response<List<ClienteDTO>>();
            var cacheKey = "clientesList";
                        
            var redisClientes = await _distributedCache.GetAsync(cacheKey);
            if (redisClientes != null)
            {
                res.Data = JsonSerializer.Deserialize<List<ClienteDTO>>(redisClientes);
            }
            else
            {
                res.Data = _mapper.Map<List<Cliente>, List<ClienteDTO>>(await _unitOfWork.ClienteRepository.GetClientesAsync());

                if (res.Data != null)
                {
                    var serializedClientes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res.Data));
                    var opt = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                    await _distributedCache.SetAsync(cacheKey, serializedClientes, opt);
                }
            }

            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Clientes Obtenidos con éxito";
                _logger.LogInfo(res.Message);
            }
            
            return res;
        }

        public Response<bool> UpdateCliente(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(cliente);

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;
                _logger.LogError(res.Message + " " + res.Errors + " " + cliente.ClienteId);
            }
            else
            {
                var cli = _mapper.Map<ClienteDTO, Cliente>(cliente);

                res.Data = _unitOfWork.ClienteRepository.UpdateCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.ClienteId);
                }

            }            

            return res;
        }

        public async Task<Response<bool>> UpdateClienteAsync(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(cliente);

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;
                _logger.LogError(res.Message + " " + res.Errors + " " + cliente.ClienteId);
            }
            else
            {
                var cli = _mapper.Map<ClienteDTO, Cliente>(cliente);

                res.Data = await _unitOfWork.ClienteRepository.UpdateClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.ClienteId);
                }

            }            

            return res;
        }
    }
}
