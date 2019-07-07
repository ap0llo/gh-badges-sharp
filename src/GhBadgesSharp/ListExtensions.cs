using System.Collections.Generic;

namespace GhBadgesSharp
{
    internal static class ListExtensions
    {
        public static void AddIfNotNull<T>(this IList<T> list, T value) where T : class
        {
            if (value != null)
                list.Add(value);
        }
    }
}
