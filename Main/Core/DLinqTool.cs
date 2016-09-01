using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.LinqTool.LinqExpression;
using Core.Models;

namespace Core
{
    public class DLinqTool : IDLinqTool
    {
        public Expression<Func<T, bool>> GetLambda<T>(T obj, SearchFilter filter)
        {
            if (filter != null && filter.rules != null)
            {
                var groupOp = filter.groupOp;
                var flag = (SearchFlag)Enum.Parse(typeof(SearchFlag), groupOp);
                var tool = new ExpressionTool();
                return tool.GetLambda(obj, filter.rules.ToList(), flag);
            }
            return (new ExpressionTool()).True<T>();
        }

        public Expression<Func<T, bool>> GetLambda<T>(T obj, string jsonFilter)
        {
            if (!string.IsNullOrEmpty(jsonFilter))
            {
                var model = JsonSerializer<SearchFilter>.ToObject(jsonFilter);
                return GetLambda(obj, model);
            }
            return (new ExpressionTool()).True<T>();
        }
        public Expression<Func<T, bool>> GetLambda<T>(T obj, List<SearchFilter> searchFilters, SearchFlag searchFlag = SearchFlag.AND)
        {
            if (searchFilters != null)
            {
                var tool = new ExpressionTool();
                return tool.GetLambda(obj, searchFilters, searchFlag);
            }
            return (new ExpressionTool()).True<T>();
        }

        public Expression<Func<T, dynamic>> GetOrderByLambda<T>(T obj, string properName)
        {
            var tool = new ExpressionTool();
            return tool.BaseExpression(obj, properName);
        }

        public Expression<Func<T, dynamic>> GetGroupByLambda<T>(T obj, string properName)
        {
            var tool = new ExpressionTool();
            return tool.BaseExpression(obj, properName);
        }
    }
}
