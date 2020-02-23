using Sieve.Models;

namespace Application.Common.Sieve.Extensions
{
    public static class FilterOperatorExtension
    {
        public static FilterOperator GetFilterOperator(this string @operator)
        {
            switch (@operator.TrimEnd('*'))
            {
                case "==":
                    return FilterOperator.Equals;
                case "!=":
                    return FilterOperator.NotEquals;
                case "<":
                    return FilterOperator.LessThan;
                case ">":
                    return FilterOperator.GreaterThan;
                case ">=":
                    return FilterOperator.GreaterThanOrEqualTo;
                case "<=":
                    return FilterOperator.LessThanOrEqualTo;
                case "@=":
                case "!@=":
                    return FilterOperator.Contains;
                case "_=":
                case "!_=":
                    return FilterOperator.StartsWith;
                default:
                    return FilterOperator.Equals;
            }
        }
    }
}