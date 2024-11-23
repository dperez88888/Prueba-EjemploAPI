namespace PruebaEjemploAPI_Backend.Dominio.DTO
{
    public class UsuarioDomDTO
    {
        public int UsuarioId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellidos { get; set; }

        public required string Password { get; set; }

        public string Token { get; set; }
    }
}
