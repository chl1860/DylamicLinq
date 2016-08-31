using System.Linq.Expressions;
using System.Reflection;

namespace Core.LinqTool.LinqExpression
{
    public class ContainsLinqExpression : ILinqExpression
    {
        public Expression ExpressionGenerator<T>(T obj, string propName, string propValue, ParameterExpression parameter)
        {
            var exprTool = new ExpressionTool();
            Expression left = exprTool.GenerateLeftExpression(obj, propName, parameter);
            MethodCallExpression callExpression = null;
            if (string.IsNullOrEmpty(propValue))
                propValue = "";
            ConstantExpression valExpression = Expression.Constant(propValue); //"val"
            if (left != null)
            {
                MethodInfo method = typeof(string).GetMethod("Contains"); //Conatains()
                callExpression = Expression.Call(left, method, valExpression);
            }

            return callExpression;
        }
    }
}