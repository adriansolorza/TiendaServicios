namespace TiendaServicios.Api.Autor.Modelo
{
    public class GradoAcademico
    {
        public Guid GradoAcademicoId { get; set; }
        public int GradAcaId{ get; set; }
        public string Nombre {get; set;} = string.Empty;
        public string CentroAcademico {get; set; } = string.Empty;
        public DateTime FechaGrado { get; set; }

        public int AutorId { get; set; }
        public AutorLibro AutorLibro { get; set; } = new AutorLibro();
    }
}
