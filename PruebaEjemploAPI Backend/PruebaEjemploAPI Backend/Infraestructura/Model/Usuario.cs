namespace PruebaEjemploAPI_Backend.Infraestructura.Model
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellidos { get; set; }

        public required string Password { get; set; }

        public string? Token { get; set; }
    }
}
