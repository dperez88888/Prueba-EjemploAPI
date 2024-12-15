using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;
using System.Collections.Generic;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuariosQuery
{

    public class GetUsuariosHandler : IRequestHandler<GetUsuariosQuery, Response<List<UsuarioDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsuariosHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<UsuarioDTO>>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
        {
            var res = new Response<List<UsuarioDTO>>();

            res.Data = _mapper.Map<List<Usuario>, List<UsuarioDTO>>(await _unitOfWork.UsuarioRepository.GetUsuariosAsync());
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Usuarios Obtenidos con éxito";
            }

            return res;
        }
    }
}
