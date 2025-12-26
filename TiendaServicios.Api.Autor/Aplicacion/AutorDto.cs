namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class AutorDto
    {
        public string AutorLibroId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
    }
}
