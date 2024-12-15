using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClienteValidator _clienteValidator;

        public UpdateClienteHandler(IUnitOfWork unitOfWork, IMapper mapper, ClienteValidator clienteValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clienteValidator = clienteValidator;
        }

        public async Task<Response<bool>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(_mapper.Map<UpdateClienteCommand, ClienteDTO>(request));

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;
            }
            else
            {
                var cli = _mapper.Map<UpdateClienteCommand, Cliente>(request);

                res.Data = await _unitOfWork.ClienteRepository.UpdateClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Actualizado con éxito";
                }

            }

            return res;
        }
    }
}
