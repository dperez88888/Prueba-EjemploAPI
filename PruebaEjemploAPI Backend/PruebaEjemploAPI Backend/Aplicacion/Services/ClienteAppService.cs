using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Dominio.DTO;
using PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Dominio.Services
{
    public class ClienteAppService : IClienteAppService
    {

        private readonly IClienteDomService _clienteDomService;
        private readonly IMapper _mapper;

        public ClienteAppService(IClienteDomService clienteDomService, IMapper mapper)
        {
            _clienteDomService = clienteDomService;
            _mapper = mapper;
        }

        public Response<bool> AddCliente(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            try
            {                
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = _clienteDomService.AddCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res; 
        }

        public async Task<Response<bool>> AddClienteAsync(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = await _clienteDomService.AddClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
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
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
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
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
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
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
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
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public Response<List<ClienteDTO>> GetClientes()
        {
            var res = new Response<List<ClienteDTO>>();

            try
            {                
                res.Data = _mapper.Map<List<ClienteDomDTO>, List<ClienteDTO>>(_clienteDomService.GetClientes());
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Clientes Obtenidos con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public async Task<Response<List<ClienteDTO>>> GetClientesAsync()
        {
            var res = new Response<List<ClienteDTO>>();

            try
            {
                res.Data = _mapper.Map<List<ClienteDomDTO>, List<ClienteDTO>>(await _clienteDomService.GetClientesAsync());
                if (res.Data != null)
                {
                    res.IsSuccess = true;
                    res.Message = "Clientes Obtenidos con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public Response<bool> UpdateCliente(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = _clienteDomService.UpdateCliente(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public async Task<Response<bool>> UpdateClienteAsync(ClienteDTO cliente)
        {
            var res = new Response<bool>();

            try
            {
                var cli = _mapper.Map<ClienteDTO, ClienteDomDTO>(cliente);

                res.Data = await _clienteDomService.UpdateClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
