using FitnessAI.Application.Common.Models;

namespace FitnessAI.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
        this IQueryable<T> queryable,
        int pageNumber,
        int pageSize)
    {
        return await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);
    }
}