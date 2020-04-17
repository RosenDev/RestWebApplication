using System.Collections;
using System.Collections.Generic;
using RestWebApplication.Common;

namespace RestWebApplication.Services.Mapping.AutoMapperExtensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> To<T>(this IEnumerable source)
        {
            ThrowHelper.ThrowIfNull(source, nameof(source));

            var destination = new List<T>();

            foreach (var item in source)
            {
                destination.Add(item.To<T>());
            }

            return destination;
        }
    }
}