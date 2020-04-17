using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using RestWebApplication.Common;

namespace RestWebApplication.Infrastructure.Helpers
{
    public static class ObjectExtensions
    {
        public static ExpandoObject Shape<TSource>(this TSource src, string fields)
        {
            ThrowHelper.ThrowIfNull(src, nameof(src));

            var propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fields.Split(",");

                foreach (var field in fieldsAfterSplit)
                {
                    var propName = field.Trim();

                    var propInfo = typeof(TSource)
                        .GetProperty(propName, BindingFlags.IgnoreCase |
                                               BindingFlags.Instance | BindingFlags.Public);

                    ThrowHelper.ThrowIfNull(propInfo, nameof(propInfo));

                    propertyInfoList.Add(propInfo);

                }
            }
            var dataShapedObject = new ExpandoObject();

            foreach (var propertyInfo in propertyInfoList)
            {
                var propValue = propertyInfo.GetValue(src);

                ((IDictionary<string, object>) dataShapedObject)
                    .Add(propertyInfo.Name, propValue);
            }

            return dataShapedObject;
        }
    }
}