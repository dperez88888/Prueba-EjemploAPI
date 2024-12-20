﻿using MediatR;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Commands.UpdateClienteCommand
{
    public sealed record UpdateClienteCommand : IRequest<Response<bool>>
    {
        public int ClienteId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellidos { get; set; }

        public string? Sexo { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public string? Direccion { get; set; }

        public string? Pais { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Email { get; set; }
    }
}
