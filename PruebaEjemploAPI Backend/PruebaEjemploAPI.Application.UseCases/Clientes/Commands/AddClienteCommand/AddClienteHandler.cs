using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Commands.AddClienteCommand
{
    public class AddClienteHandler : IRequestHandler<AddClienteCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClienteValidator _clienteValidator;

        public AddClienteHandler(IUnitOfWork unitOfWork, IMapper mapper, ClienteValidator clienteValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clienteValidator = clienteValidator;
        }

        public async Task<Response<bool>> Handle(AddClienteCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();

            var validation = _clienteValidator.Validate(_mapper.Map<AddClienteCommand, ClienteDTO>(request));

            if (!validation.IsValid)
            {
                res.IsSuccess = false;
                res.Message = "Errores de Validación";
                res.Errors = validation.Errors;                
            }
            else
            {
                var cli = _mapper.Map<AddClienteCommand, Cliente>(request);

                res.Data = await _unitOfWork.ClienteRepository.AddClienteAsync(cli);
                if (res.Data)
                {
                    res.IsSuccess = true;
                    res.Message = "Cliente Insertado con éxito";
                                    }
            }

            return res;
        }
    }
}
