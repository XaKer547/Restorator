using Microsoft.EntityFrameworkCore;
using Restorator.Domain.Models;

namespace Restorator.Application.Services.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> AsPageAsync<T>(this IQueryable<T> query, int currentPage, int pageSize)
        {
            var items = await query.Skip((currentPage - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();

            return new PaginatedList<T>(currentPage, query.Count(), pageSize, items);
        }
    }
}
