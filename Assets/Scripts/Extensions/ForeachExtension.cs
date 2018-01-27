using System;
using System.Collections.Generic;

namespace System.Linq
{
    public static class ForeachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
