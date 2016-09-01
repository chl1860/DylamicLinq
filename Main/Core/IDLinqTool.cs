using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;

namespace Core.LinqTool
{
    public interface IDLinqTool
    {
        Expression<Func<T, bool>> GetLambda<T>(T obj, SearchFilter filter);
        Expression<Func<T, bool>> GetLambda<T>(T obj, string jsonFilter);
        Expression<Func<T, bool>> GetLambda<T>(T obj, List<SearchFilter> searchFilters, SearchFlag searchFlag = SearchFlag.AND);
        Expression<Func<T, dynamic>> GetOrderByLambda<T>(T obj, string properName);
        Expression<Func<T, dynamic>> GetGroupByLambda<T>(T obj, string properName);
    }
}
