using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuarioQuery
{

    public class GetUsuarioHandler : IRequestHandler<GetUsuarioQuery, Response<UsuarioDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UsuarioDTO>> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            var res = new Response<UsuarioDTO>();

            res.Data = _mapper.Map<Usuario, UsuarioDTO>(await _unitOfWork.UsuarioRepository.GetUsuarioAsync(request.UsuarioId));
            if (res.Data != null)
            {
                res.IsSuccess = true;
                res.Message = "Usuario Obtenido con éxito";
            }

            return res;
        }
    }
}
