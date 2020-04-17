using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using RestWebApplication.Common;

namespace RestWebApplication.Infrastructure.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(
            this IEnumerable<TSource> src,
            string fields)
        {
            ThrowHelper.ThrowIfNull(src,nameof(src));
            
            var expandoObjects = new List<ExpandoObject>();
            
            var propertyInfoList =  new List<PropertyInfo>();

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
                    
                    ThrowHelper.ThrowIfNull(propInfo,nameof(propInfo));
                    
                    propertyInfoList.Add(propInfo);
                    
                }
            }

            foreach (TSource srcObject in src)
            {
                var dataShapedObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propValue = propertyInfo.GetValue(srcObject);
                    
                    ((IDictionary<string,object>)dataShapedObject)
                        .Add(propertyInfo.Name,propValue);
                }
                expandoObjects.Add(dataShapedObject);
            }

            return expandoObjects;
        }
    }
}