namespace PruebaEjemploAPI_Backend.Aplicacion.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellidos { get; set; }

        public required string Password { get; set; }

        public string Token { get; set; }
    }
}
