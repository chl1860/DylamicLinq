using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;

namespace Core.LinqTool.LinqExpression
{
    public class ExpressionTool
    {
        /// <summary>
        /// Get expression by searchfilters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="searchFilters"></param>
        /// <param name="logicFlag"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetLambda<T>(T obj, List<SearchFilter> searchFilters, SearchFlag logicFlag = SearchFlag.AND)
        {
            var parameter = Expression.Parameter(typeof(T), "i");
            Expression totalExpression = null;

            if (searchFilters.Count > 0)
            {
                foreach (var filter in searchFilters)
                {
                    if (filter == null || filter.rules == null)
                        continue;
                    Expression searchExpr = null;
                    List<Expression> exprList = GetExpressionsByFilterList(obj, filter.rules.ToList(), parameter);
                    if (exprList.Count > 0)
                    {
                        exprList.RemoveNullItem();
                        searchExpr = string.Equals("AND", filter.groupOp.ToUpper()) ? GenerateAndExpression(exprList) : GenerateOrElseExpression(exprList);
                    }

                    if (searchExpr != null)
                    {
                        totalExpression = totalExpression == null ? searchExpr : (string.Equals(logicFlag.ToString(), "AND") ? Expression.AndAlso(totalExpression, searchExpr) : Expression.OrElse(totalExpression, searchExpr));
                    }
                }
            }

            return totalExpression != null ? Expression.Lambda<Func<T, bool>>(totalExpression, parameter) : True<T>();
        }

        /// <summary>
        /// get expression list by rules
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="ruleList"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private List<Expression> GetExpressionsByFilterList<T>(T obj, List<Filter> ruleList, ParameterExpression parameter)
        {
            var expression = new List<Expression>();
            foreach (var rules in ruleList)
            {
                ILinqExpression linqExpr = Factory.GetInstance().ExpressionCreator(rules.op);
                if (linqExpr == null)
                    return expression;
                Expression expr = linqExpr.ExpressionGenerator(obj, rules.field, rules.data, parameter);
                expression.Add(expr);
            }
            return expression;
        }
        public Expression<Func<T, bool>> GetLambda<T>(T obj, List<Filter> ruleList, SearchFlag logicFlag = SearchFlag.AND)
        {
            var parameter = Expression.Parameter(typeof(T), "i");
            var expression = GetExpressionsByFilterList(obj, ruleList, parameter);
            return string.Equals("AND", logicFlag.ToString().ToUpper())
                ? GetAndLambda<T>(parameter, expression)
                : GetOrLambda<T>(parameter, expression);
        }
        private Expression<Func<T, bool>> GetAndLambda<T>(ParameterExpression parameter, List<Expression> exprList)
        {
            exprList.RemoveNullItem();
            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.AndAlso(whereExpr, expr);
            }
            if (whereExpr != null)
            {
                return Expression.Lambda<Func<T, bool>>(whereExpr, parameter);
            }
            return True<T>();
        }
        private Expression<Func<T, bool>> GetOrLambda<T>(ParameterExpression parameter, List<Expression> exprList)
        {
            exprList.RemoveNullItem();
            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.OrElse(whereExpr, expr);
            }
            if (whereExpr != null)
            {
                return Expression.Lambda<Func<T, bool>>(whereExpr, parameter);
            }
            return True<T>();
        }

        public Expression GenerateLeftExpression<T>(T obj, string propName, ParameterExpression parameter)
        {
            ClassAdapter classAdapter = new ClassAdapter();
            var propDic = classAdapter.GetPropertyDic(obj);
            Expression left = null;

            if (!propName.Contains(".") && propDic.ContainsKey(propName)) //i=>i.propName
            {
                left = Expression.Property(parameter, propName);
            }
            if (propName.Contains(".")) //i=> i.propName.prop...
            {
                var propArray = propName.Split(new[] { '.' });
                if (propDic.ContainsKey(propArray[0]))
                {
                    left = parameter;
                    foreach (var s in propArray)
                    {
                        left = Expression.Property(left, s);
                    }
                }
            }

            return left;
        }
        private Expression GenerateAndExpression(List<Expression> exprList)
        {
            exprList.RemoveNullItem();
            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.AndAlso(whereExpr, expr);
            }
            return whereExpr;
        }
        private Expression GenerateOrElseExpression(List<Expression> exprList)
        {
            exprList.RemoveNullItem();
            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.OrElse(whereExpr, expr);
            }
            return whereExpr;

        }
        public ConstantExpression GetConstantExpression(Type type, string propValue)
        {
            ConstantExpression val;
            if (string.IsNullOrEmpty(propValue) || propValue.Trim().ToLower() == "null")
            {
                val = Expression.Constant(null, type);
                return val;
            }
            try
            {
                if (type == typeof(bool?) ||
                    type == typeof(Boolean))
                    val = Expression.Constant(Convert.ToBoolean(propValue), type);
                else if (type == typeof(int?) ||
                    type == typeof(int))
                    val = Expression.Constant(Convert.ToInt32(propValue), type);
                else if (type == typeof(decimal?) ||
                    type == typeof(Decimal))
                    val = Expression.Constant(Convert.ToDecimal(propValue), type);
                else if (type == typeof(DateTime?) ||
                    type == typeof(DateTime))
                    val = Expression.Constant(Convert.ToDateTime(propValue), type);
                else if (type == typeof(double?) ||
                    type == typeof(Double))
                    val = Expression.Constant(Convert.ToDouble(propValue), type);
                else
                    val = Expression.Constant(propValue);
            }
            catch
            {
                val = Expression.Constant(null, type);
            }
            return val;
        }
        public Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }
        public Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public Expression<Func<T, dynamic>> BaseExpression<T>(T obj, string propName)//get i=>xxx
        {
            var parameter = Expression.Parameter(typeof(T), "i");
            var left = GenerateLeftExpression(obj, propName, parameter);
            left = Expression.Convert(left, typeof(object));
            return Expression.Lambda<Func<T, dynamic>>(left, parameter);
        }

    }
}
