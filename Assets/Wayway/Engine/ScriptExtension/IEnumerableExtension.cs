using System;
using System.Collections.Generic;
using System.Linq;

namespace Wayway.Engine
{
    public static class IEnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list == null || list.Count() == 0;
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
                action(item);
        }
    }
}