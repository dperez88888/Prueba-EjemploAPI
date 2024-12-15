using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClientesQuery
{

    public class GetClientesHandler : IRequestHandler<GetClientesQuery, Response<List<ClienteDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClientesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<ClienteDTO>>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            var res = new Response<List<ClienteDTO>>();
                        
            res.Data = _mapper.Map<List<Cliente>, List<ClienteDTO>>(await _unitOfWork.ClienteRepository.GetClientesAsync());

            if (res.Data != null)
            {
                var serializedClientes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res.Data));
                var opt = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
            }
            

            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Clientes Obtenidos con éxito";
            }

            return res;
        }
    }
}
