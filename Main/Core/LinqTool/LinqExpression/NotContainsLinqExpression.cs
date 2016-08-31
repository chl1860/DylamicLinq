using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.LinqTool.LinqExpression
{
    public class NotContainsLinqExpression : ILinqExpression
    {
        public Expression ExpressionGenerator<T>(T obj, string propName, string propValue, System.Linq.Expressions.ParameterExpression parameter)
        {
            ExpressionTool exprTool = new ExpressionTool();
            Expression left = exprTool.GenerateLeftExpression(obj, propName, parameter);
            MethodCallExpression callExpression = null;
            ConstantExpression valExpression = Expression.Constant(propValue); //"val"
            if (left != null)
            {
                MethodInfo method = typeof(string).GetMethod("Contains"); //Conatains()
                callExpression = Expression.Call(left, method, valExpression);
            }
            ConstantExpression val = Expression.Constant(Convert.ToBoolean("false"), typeof(bool));
            return Expression.Equal(callExpression, val);
        }
    }
}
