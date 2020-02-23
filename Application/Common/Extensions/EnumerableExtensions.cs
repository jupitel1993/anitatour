using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source, string property, bool asc)
        {
            var prop = typeof(T).GetProperty(property);
            if (prop == null)
            {
                throw new ArgumentNullException();
            }

            var result = asc
                ? source.ToList().OrderBy(x => prop.GetValue(x))
                : source.ToList().OrderByDescending(x => prop.GetValue(x));
            
            return result;
        }

        public static IList SelectByProperty<T>(this IEnumerable<T> source, string property)
        {
            var prop = typeof(T).GetProperty(property);
            if (prop == null)
            {
                throw new ArgumentNullException();
            }

            return source.Select(x => prop.GetValue(x)).ToList();
        }
    }
}
