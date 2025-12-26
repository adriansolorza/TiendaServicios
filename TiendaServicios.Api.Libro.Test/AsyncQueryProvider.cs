using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Test
{
    internal class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;
        public AsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultadotipo = typeof(TResult).GetGenericArguments()[0];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ejecucionResultado = typeof(IQueryProvider)
                .GetMethod(
                   name: nameof(IQueryProvider.Execute),
                   genericParameterCount: 1,
                   types: new[] { typeof(Expression) }
                )
                .MakeGenericMethod(resultadotipo)
                .Invoke(this, new[] { expression });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))?
                .MakeGenericMethod(resultadotipo).Invoke(null, new[] { ejecucionResultado });
        }
    }
}
