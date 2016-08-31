using System.Collections.Generic;
using System.Linq;

namespace Core.LinqTool.LinqExpression
{
    public class Factory
    {
        private static Factory m_Factory;
        private static readonly object Object = new object();
        private readonly Dictionary<string, ILinqExpression> m_ExpressionDict = new Dictionary<string, ILinqExpression>();
        private Factory()
        {
            //normal
            m_ExpressionDict.Add("eq", new EqualLinqExpression()); //equal
            m_ExpressionDict.Add("ne", new NotEqualLinqExpression()); //not equal
            m_ExpressionDict.Add("cn", new ContainsLinqExpression());//contains
            m_ExpressionDict.Add("nc", new NotContainsLinqExpression()); //not contains
            //not mormal
            m_ExpressionDict.Add("lt", new LessLinqExpression()); //less
            m_ExpressionDict.Add("le", new LessOrEqLinqExpression()); //less or equal
            m_ExpressionDict.Add("gt", new GreaterThanLinqExpression()); //greater than
            m_ExpressionDict.Add("ge", new GreaterOrEqExpressionLinqExpression()); //greater or equal
            //expressionDict.Add("bw", new GreaterThanLinqExpression()); //begins with
            //expressionDict.Add("bn", new GreaterOrEqExpressionLinqExpression()); //does not begin with
            //expressionDict.Add("in", new LessLinqExpression()); //is in
            //expressionDict.Add("ni", new LessOrEqLinqExpression()); //is not in
            //expressionDict.Add("ew", new LessLinqExpression()); //ends with
            //expressionDict.Add("en", new LessOrEqLinqExpression()); //does not end with
        }

        public ILinqExpression ExpressionCreator(string type)
        {
            if (m_ExpressionDict.Count(p => p.Key == type) > 0)
                return m_ExpressionDict[type];
            return null;
        }

        public static Factory GetInstance()
        {
            if (m_Factory == null)
            {
                lock (Object)
                {
                    if (m_Factory == null)
                        m_Factory = new Factory();
                }
            }
            return m_Factory;
        }
    }
}
