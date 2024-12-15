using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.AddUsuarioCommand
{
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _UsuarioValidator;

        public UpdateUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator UsuarioValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UsuarioValidator = UsuarioValidator;
        }

        public async Task<Response<bool>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();

            var validation = _UsuarioValidator.Validate(_mapper.Map<UpdateUsuarioCommand, UsuarioDTO>(request));

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;                
            }
            else
            {
                var cli = _mapper.Map<UpdateUsuarioCommand, Usuario>(request);

                res.Data = await _unitOfWork.UsuarioRepository.UpdateUsuarioAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Usuario Insertado con éxito";
                }
            }

            return res;
        }
    }
}
