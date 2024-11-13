using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Dominio.DTO;
using PruebaEjemploAPI_Backend.Transversal.Common;
using PruebaEjemploAPI_Backend.Aplicacion.Validators;
using k8s.KubeConfigModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public class ClienteAppService : IClienteAppService
    {

        private readonly IClienteDomService _clienteDomService;
        private readonly IMapper _mapper;
        private readonly ClienteValidator _clienteValidator;
        private readonly IAppLogger<IClienteAppService> _logger;
        private readonly IDistributedCache _distributedCache;

        public ClienteAppService(IClienteDomService clienteDomService, IMapper mapper, ClienteValidator clienteValidator, IAppLogger<IClienteAppService> logger, IDistributedCache distributedCache)
        {
            _clienteDomService = clienteDomService;
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
            try
            {                
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = _clienteDomService.AddCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
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

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = await _clienteDomService.AddClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + cliente.Nombre + " " + cliente.Apellidos);
            }

            return res;
        }

        public Response<bool> DeleteCliente(int clienteId)
        {
            var res = new Response<bool>();

            try
            {                
                res.Data = _clienteDomService.DeleteCliente(clienteId);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Borrado con éxito";
                    _logger.LogInfo(res.Message + " " + clienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + clienteId);
            }

            return res;
        }

        public async Task<Response<bool>> DeleteClienteAsync(int clienteId)
        {
            var res = new Response<bool>();

            try
            {
                res.Data = await _clienteDomService.DeleteClienteAsync(clienteId);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Borrado con éxito";
                    _logger.LogInfo(res.Message + " " + clienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + clienteId);
            }

            return res;
        }

        public Response<ClienteDTO> GetCliente(int clienteId)
        {
            var res = new Response<ClienteDTO>();

            try
            {                
                res.Data = _mapper.Map<ClienteDomDTO, ClienteDTO> (_clienteDomService.GetCliente(clienteId));
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Obtenido con éxito";
                    _logger.LogInfo(res.Message + " " + clienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + clienteId);
            }

            return res;
        }

        public async Task<Response<ClienteDTO>> GetClienteAsync(int clienteId)
        {
            var res = new Response<ClienteDTO>();

            try
            {
                res.Data = _mapper.Map<ClienteDomDTO, ClienteDTO>(await _clienteDomService.GetClienteAsync(clienteId));
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Obtenido con éxito";
                    _logger.LogInfo(res.Message + " " + clienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + clienteId);
            }

            return res;
        }

        public Response<List<ClienteDTO>> GetClientes()
        {
            var res = new Response<List<ClienteDTO>>();
            var cacheKey = "clientesList";

            try
            {      
                var redisClientes = _distributedCache.Get(cacheKey);
                if (redisClientes != null)
                {
                    res.Data = JsonSerializer.Deserialize<List<ClienteDTO>>(redisClientes);
                }
                else
                {                    
                    res.Data = _mapper.Map<List<ClienteDomDTO>, List<ClienteDTO>>(_clienteDomService.GetClientes());
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

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message);
            }

            return res;
        }

        public async Task<Response<List<ClienteDTO>>> GetClientesAsync()
        {
            var res = new Response<List<ClienteDTO>>();
            var cacheKey = "clientesList";

            try
            {
                var redisClientes = await _distributedCache.GetAsync(cacheKey);
                if (redisClientes != null)
                {
                    res.Data = JsonSerializer.Deserialize<List<ClienteDTO>>(redisClientes);
                }
                else
                {
                    res.Data = _mapper.Map<List<ClienteDomDTO>, List<ClienteDTO>>(await _clienteDomService.GetClientesAsync());

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

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message);
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

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = _clienteDomService.UpdateCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.ClienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + cliente.ClienteId);
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

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = await _clienteDomService.UpdateClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                    _logger.LogInfo(res.Message + " " + cliente.ClienteId);
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                _logger.LogError(res.Message + " " + cliente.ClienteId);
            }

            return res;
        }
    }
}
