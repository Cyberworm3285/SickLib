using System.Collections.Generic;
using System;

using SickLib.Collections.SickLinkedList;
using SickLib.Collections.Trees.SickTree;

namespace SickLib.LINQ_Extensions
{
    public static class TypeConverter
    {
        public static SickLinkedList<T> ToSickLinkedList<T>(this IEnumerable<T> en) => new SickLinkedList<T>(en);

        public static SickTree<TItem, TDecision> ToSickTree<TSource, TItem, TDecision>(this IEnumerable<TSource> en, Func<TSource, TItem> itemSelector, Func<TSource, TDecision[]> pathSelector)
        {
            var result = new SickTree<TItem, TDecision>();

            using (var enu = en.GetEnumerator())
            {
                while (enu.MoveNext())
                {
                    result.AddPath(itemSelector(enu.Current), pathSelector(enu.Current));
                }
            }

            return result;
        }
    }
}
