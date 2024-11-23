namespace PruebaEjemploAPI.Application.DTO
{
    public class ClienteDTO
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
