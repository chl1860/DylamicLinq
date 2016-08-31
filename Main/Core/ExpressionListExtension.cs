using System.Collections.Generic;

namespace Core
{
    public static class ExpressionListExtension
    {
        public static List<T> RemoveNullItem<T>(this List<T> list)
        {
            list.RemoveAll(o => o.Equals(null));
            return list;
        }

    }
}
