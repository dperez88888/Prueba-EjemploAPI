using PruebaEjemploAPI_Backend.Model;

namespace PruebaEjemploAPI_Backend.Mapper
{
    // Clase que mapea la clase cliente
    public static class ClienteEntityMapper
    {
        public static Cliente Map(Cliente cliente)
        {
            return new Cliente
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                CodigoPostal = cliente.CodigoPostal,
                Sexo = cliente.Sexo,
                FechaNacimiento = cliente.FechaNacimiento,
                Direccion = cliente.Direccion,
                Pais = cliente.Pais,
                Email = cliente.Email
            };
        }
    }
}
