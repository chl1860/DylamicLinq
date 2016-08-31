using System.Linq.Expressions;

namespace Core.LinqTool.LinqExpression
{
    public class NotEqualLinqExpression : ILinqExpression
    {
        public Expression ExpressionGenerator<T>(T obj, string propName, string propValue, ParameterExpression parameter)
        {
            var exprTool = new ExpressionTool();
            Expression left = exprTool.GenerateLeftExpression(obj, propName, parameter);
            var val = exprTool.GetConstantExpression(left.Type, propValue);
            return Expression.NotEqual(left, val);
        }
    }
}
