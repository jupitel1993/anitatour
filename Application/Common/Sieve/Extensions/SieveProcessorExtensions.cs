using System.Linq;
using Domain.Common;
using Sieve.Models;
using Sieve.Services;

namespace Application.Common.Sieve.Extensions
{
    public static class SieveProcessorExtensions
    {
        public static IQueryable<TEntity> Apply<TEntity>(this ISieveProcessor sieveProcessor, SieveModel model,
            IQueryable<TEntity> source, out int totalCount, object[] dataForCustomMethods = null) where TEntity : AuditableEntity
        {
            source = sieveProcessor.Apply(model, source, dataForCustomMethods, applyPagination: false);

            totalCount = source.Count();

            source = sieveProcessor.Apply(model, source, dataForCustomMethods, applyFiltering: false, applySorting: false);

            return source;
        }
    }
}