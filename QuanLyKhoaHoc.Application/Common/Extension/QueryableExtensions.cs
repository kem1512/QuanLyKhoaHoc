using QuanLyKhoaHoc.Application.Common.Models;
using System.Linq.Expressions;

namespace QuanLyKhoaHoc.Application.Common.Extension
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> source, QueryModel queryParameters, bool applyPagination = true, bool applyFilter = true, bool applySort = true)
        {
            if (applyFilter && !string.IsNullOrEmpty(queryParameters.Filters))
            {
                var filters = queryParameters.Filters.Split(',');
                foreach (var filter in filters)
                {
                    source = ApplyFilter(source, filter);
                }
            }

            if (applySort && !string.IsNullOrEmpty(queryParameters.Sorts))
            {
                var sorts = queryParameters.Sorts.Split(',');
                foreach (var sort in sorts)
                {
                    source = ApplySort(source, sort);
                }
            }

            // Kiểm tra applyPagination trước khi thực hiện phân trang
            if (applyPagination && queryParameters.Page.HasValue && queryParameters.PageSize.HasValue)
            {
                int page = queryParameters.Page.Value;
                int pageSize = queryParameters.PageSize.Value;

                source = source.Skip((page - 1) * pageSize).Take(pageSize);
            }

            return source;
        }

        private static IQueryable<T> ApplyFilter<T>(IQueryable<T> source, string filter)
        {
            var parts = filter.Split(new char[] { '>', '=', '<', '@' }, 2);
            if (parts.Length != 2) throw new ArgumentException("Invalid filter format");

            var propertyName = parts[0].Trim();
            var value = parts[1].Trim();

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));

            Expression comparison;
            if (filter.Contains(">"))
            {
                comparison = Expression.GreaterThan(property, constant);
            }
            else if (filter.Contains("<"))
            {
                comparison = Expression.LessThan(property, constant);
            }
            else if (filter.Contains("="))
            {
                comparison = Expression.Equal(property, constant);
            }
            else if (filter.Contains("@"))
            {
                comparison = Expression.Call(property, "Contains", null, constant);
            }
            else
            {
                throw new ArgumentException("Invalid filter operator");
            }

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return source.Where(lambda);
        }

        private static IQueryable<T> ApplySort<T>(IQueryable<T> source, string sort)
        {
            var descending = sort.StartsWith("-");
            var propertyName = descending ? sort.Substring(1) : sort;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable).GetMethods().First(
                m => m.Name == methodName && m.GetParameters().Length == 2);

            var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { source, lambda });
        }
    }
}
