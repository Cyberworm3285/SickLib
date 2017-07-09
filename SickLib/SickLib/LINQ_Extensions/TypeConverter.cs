using System.Collections.Generic;
using SickLib.Collections.SickList;


namespace SickLib.LINQ_Extensions
{
    public static class TypeConverter
    {
        public static SickList<T> ToSickList<T>(this IEnumerable<T> en) => new SickList<T>(en);
    }
}
