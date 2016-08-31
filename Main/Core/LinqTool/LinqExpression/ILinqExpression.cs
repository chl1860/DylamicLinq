using System.Linq.Expressions;

namespace Core.LinqTool.LinqExpression
{
    public interface ILinqExpression
    {
        /// <summary>
        /// expression realization 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Expression ExpressionGenerator<T>(T obj, string propName, string propValue, ParameterExpression parameter);
    }
}
