using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Common.Sieve
{
    public class AppSieveProcessor : SieveProcessor
    {
        public AppSieveProcessor(
            IOptions<SieveOptions> options,
            ISieveCustomSortMethods sortMethods,
            ISieveCustomFilterMethods filterMethods)
            : base(options, sortMethods, filterMethods)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IAppSieveProfile)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(nameof(IAppSieveProfile.MapProperties));
                methodInfo?.Invoke(instance, new object[] { mapper });
            }

            return mapper;
        }
    }
}