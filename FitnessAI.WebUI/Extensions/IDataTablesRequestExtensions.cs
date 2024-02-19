using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using FitnessAI.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Extensions;

public static class DataTablesRequestExtensions
{
    public static int GetPageNumber(this IDataTablesRequest request)
    {
        return request.Start / request.Length + 1;
    }

    public static string GetOrderInfo(this IDataTablesRequest request)
    {
        var columnSort = request?.Columns?.FirstOrDefault(x => x.Sort != null);
        var orderDirection = string.Empty;
        var columnName = string.Empty;

        if (columnSort != null)
        {
            columnName = columnSort.Name;
            orderDirection = columnSort.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
        }

        return $"{columnName} {orderDirection}";
    }

    public static IActionResult GetResponse<T>(this IDataTablesRequest request, PaginatedList<T> paginatedList)
    {
        var response = DataTablesResponse.Create(request, paginatedList.TotalCount, paginatedList.TotalCount,
            paginatedList.Items);

        return new DataTablesJsonResult(response, true);
    }
}