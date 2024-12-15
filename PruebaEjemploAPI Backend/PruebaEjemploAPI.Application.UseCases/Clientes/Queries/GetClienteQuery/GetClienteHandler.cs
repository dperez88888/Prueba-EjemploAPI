using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClienteQuery
{

    public class GetClienteHandler : IRequestHandler<GetClienteQuery, Response<ClienteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClienteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<ClienteDTO>> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            var res = new Response<ClienteDTO>();

            res.Data = _mapper.Map<Cliente, ClienteDTO>(await _unitOfWork.ClienteRepository.GetClienteAsync(request.ClienteId));
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Obtenido con éxito";
            }

            return res;
        }
    }
}
