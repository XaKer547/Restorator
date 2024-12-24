namespace Restorator.Application.Services.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> AsPage<T>(this IQueryable<T> query, int currentPage, int pageSize)
        {
            return query.Skip((currentPage - 1) * pageSize)
                 .Take(pageSize);
        }
    }
}
