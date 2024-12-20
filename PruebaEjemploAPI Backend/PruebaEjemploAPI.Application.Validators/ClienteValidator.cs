﻿using FluentValidation;
using PruebaEjemploAPI.Application.DTO;
using Cst = PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDTO>
    {

        public ClienteValidator()
        {
            RuleFor(c => c.Nombre).NotNull().NotEmpty().MaximumLength(Cst.Constants.EMAIL_MAX_LENGTH);
            RuleFor(c => c.Apellidos).NotNull().NotEmpty().MaximumLength(Cst.Constants.APELLIDOS_MAX_LENGTH);
            RuleFor(c => c.Sexo).NotNull().NotEmpty().MaximumLength(Cst.Constants.SEXO_MAX_LENGTH);
            RuleFor(c => c.FechaNacimiento).NotNull().NotEmpty();
            RuleFor(c => c.Pais).NotNull().NotEmpty();
            RuleFor(c => c.Direccion).MaximumLength(Cst.Constants.DIRECCION_MAX_LENGTH);
            RuleFor(c => c.CodigoPostal).MaximumLength(Cst.Constants.CP_MAX_LENGTH);
            RuleFor(c => c.Email).MaximumLength(Cst.Constants.EMAIL_MAX_LENGTH);
        }
    }
}
