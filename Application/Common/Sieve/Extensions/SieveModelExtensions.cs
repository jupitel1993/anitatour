using Sieve.Models;

namespace Application.Common.Sieve.Extensions
{
    public static class SieveModelExtensions
    {
        private static SieveModel Copy(this SieveModel model)
        {
            return new SieveModel()
            {
                Filters = model.Filters,
                Page = model.Page,
                PageSize = model.PageSize,
                Sorts = model.Sorts
            };
        }

        public static SieveModel AddFilter(this SieveModel model, string key, string op = null, string value = null)
        {
            var copy = model.Copy();

            var newFilter = $"{key}{op}{value}";
            copy.Filters = !string.IsNullOrEmpty(copy.Filters) ? copy.Filters + "," + newFilter : newFilter;

            return copy;
        }
    }
}