namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public int AutorId { get; set; }
        public Guid AutorLibroId {get; set;}
        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public DateTime? FechaNacimiento { get; set; }

        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; } = new HashSet<GradoAcademico>();
    }
}
