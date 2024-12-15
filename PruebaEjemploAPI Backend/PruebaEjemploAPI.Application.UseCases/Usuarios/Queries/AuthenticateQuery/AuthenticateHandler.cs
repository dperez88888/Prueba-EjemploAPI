using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;
using PruebaEjemploAPI.Transversal.Exceptions;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.AuthenticateQuery
{

    public class AuthenticateHandler : IRequestHandler<AuthenticateQuery, Response<UsuarioDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _usuarioValidator;

        public AuthenticateHandler(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator usuarioValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
        }

        public async Task<Response<UsuarioDTO>> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<UsuarioDTO>();

            var validation = _usuarioValidator.Validate(new UsuarioDTO() { Nombre = request.Nombre, Password = request.Password, Apellidos = "x" });

            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.IsSuccess = false;
                response.Errors = validation.Errors;
            }
            else
            {
                try
                {
                    var user = await _unitOfWork.UsuarioRepository.AuthenticateAsync(request.Nombre, request.Password);
                    response.Data = _mapper.Map<Usuario, UsuarioDTO>(user);
                    response.IsSuccess = true;
                    response.Message = "Autenticación válida";
                }
                catch (UsuarioNotFoundException ex)
                {
                    response.IsSuccess = true;
                    response.Message = ex.Message;
                }
            }

            return response;
        }
    }
}
