namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime FechaCreacionSeeion { get; set; }

        public List<CarritoDetalleDto> ListaProductos { get; set; }

    }
}
