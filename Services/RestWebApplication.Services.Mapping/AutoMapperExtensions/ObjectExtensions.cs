using AutoMapper;
using RestWebApplication.Common;

namespace RestWebApplication.Services.Mapping.AutoMapperExtensions
{
    public static class ObjectExtensions
    {
        public static T To<T>(this object source)
        {
            ThrowHelper.ThrowIfNull(source, nameof(source));

            return Mapper.Map<T>(source);
        }
    }
}