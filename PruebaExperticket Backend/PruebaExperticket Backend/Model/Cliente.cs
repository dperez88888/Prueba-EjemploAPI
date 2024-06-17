namespace PruebaExperticket_Backend.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }

        public String Sexo { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public String Direccion { get; set; }

        public String Pais { get; set;}

        public String CodigoPostal { get; set; }

        public String Email { get; set; }
    }
}
