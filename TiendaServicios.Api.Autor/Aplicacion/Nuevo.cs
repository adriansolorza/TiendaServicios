using FluentValidation;
using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest { 
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
                RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellidos = request.Apellido,
                    AutorLibroId = Guid.NewGuid()
                };

                _contexto.AutorLibro.Add(autorLibro);
                var noTransacciones = await _contexto.SaveChangesAsync(cancellationToken);

                if (noTransacciones > 0) return Unit.Value;

                throw new Exception("No se puede insertar autor libro");
            }
        }
    }
}
