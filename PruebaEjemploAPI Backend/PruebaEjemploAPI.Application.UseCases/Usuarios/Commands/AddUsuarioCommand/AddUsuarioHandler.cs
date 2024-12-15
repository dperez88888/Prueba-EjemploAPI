using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.AddUsuarioCommand
{
    public class AddUsuarioHandler : IRequestHandler<AddUsuarioCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _UsuarioValidator;

        public AddUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator UsuarioValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UsuarioValidator = UsuarioValidator;
        }

        public async Task<Response<bool>> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();

            var validation = _UsuarioValidator.Validate(_mapper.Map<AddUsuarioCommand, UsuarioDTO>(request));

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;                
            }
            else
            {
                var cli = _mapper.Map<AddUsuarioCommand, Usuario>(request);

                res.Data = await _unitOfWork.UsuarioRepository.AddUsuarioAsync(cli);
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
